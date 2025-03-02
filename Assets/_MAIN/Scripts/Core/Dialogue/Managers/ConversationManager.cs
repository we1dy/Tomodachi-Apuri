using System.Collections;
using System.Collections.Generic;
using CHARACTERS;
using COMMANDS;
using DIALOGUE.LogicalLines;
using UnityEngine;

namespace DIALOGUE
{
    public class ConversationManager
    {
        private DialogueSystem dialogueSystem => DialogueSystem.instance;

        private Coroutine process = null;
        public bool isRunning => process != null;

        public TextArchitect architect = null;
        private bool userPrompt = false;

        private TagManager tagManager;
        private LogicalLineManager logicalLineManager;

        public ConversationManager(TextArchitect architect)
        {
            this.architect = architect;
            dialogueSystem.onUserPrompt_Next += OnUserPrompt_Next;

            tagManager = new TagManager();
            logicalLineManager = new LogicalLineManager();
        }

        private void OnUserPrompt_Next()
        {
            userPrompt = true;
        }
        
        public Coroutine StartConversation(List<string> conversation)
        {
            StopConversation();

            process = dialogueSystem.StartCoroutine(RunningCoversation(conversation));

            return process;
        }

        public void StopConversation() 
        {
            if (!isRunning)
                return;
            
            dialogueSystem.StopCoroutine(process);
            process = null;
        }

        IEnumerator RunningCoversation(List<string> conversation)
        {
            for (int i = 0; i < conversation.Count; i++)
            {
                //Dont show any blank lines or run any logic
                if (string.IsNullOrWhiteSpace(conversation[i]))
                    continue;

                DIALOGUE_LINE line = DialogueParser.Parse(conversation[i]);

                if (logicalLineManager.TryGetLogic(line, out Coroutine logic))
                {
                    yield return logic;
                }
                else
                {
                    //Show dialogue
                    if (line.hasDialogue)
                        yield return Line_RunDialogue(line);

                    //Run any commands
                    if (line.hasCommands)
                        yield return Line_RunCommands(line);

                    //wait for user input if dialogue was in this line
                    if (line.hasDialogue)
                    {
                        //wait for user input
                        yield return WaitForUserInput();

                        CommandManager.instance.StopAllProcesses();
                    }
                }
            }
        }


        private void HandleSpeakerLogic(DL_SPEAKER_DATA speakerData)
        {
            bool characterMustBeCreated = (speakerData.makeCharacterEnter || speakerData.isCastingPosition || speakerData.isCastingExpressions);        

            Character character = CharacterManager.instance.GetCharacter(speakerData.name, createIfDoesNotExist: characterMustBeCreated);

            if (speakerData.makeCharacterEnter && (!character.isVisible && !character.isRevealing))
                character.Show();

            //Add character name to the UI
            dialogueSystem.ShowSpeakerName(tagManager.Inject(speakerData.displayName));

            //Now cutomizze the dialogue for this characterr - if applicable
            DialogueSystem.instance.ApplySpeakerDataToDialogueContainer(speakerData.name);

            //if (speakerData.isCastingPosition)
            //    character.MoveToPosition(speakerData.castPosition);

            if (speakerData.isCastingExpressions)
            {
                foreach(var ce in speakerData.CastExpressions)
                    character.OnReceiveCastingExpression(ce.layer, ce.expression);
            }
        }

        IEnumerator Line_RunCommands(DIALOGUE_LINE line)
        {
            List<DL_COMMAND_DATA.Command> commands = line.commandsData.commands;

            foreach(DL_COMMAND_DATA.Command command in commands)
            {
                if (command.waitForCompletion || command.name == "wait")
                {
                    CoroutineWrapper cw = CommandManager.instance.Execute(command.name, command.arguments);
                    while (!cw.IsDone)
                    {
                        if (userPrompt)
                        {
                            CommandManager.instance.StopCurrentProcess();   
                            userPrompt = false;
                        }
                        yield return null;
                    }
                }
                else
                    CommandManager.instance.Execute(command.name, command.arguments);
            }

            yield return null;
        }

        IEnumerator BuildLineSegments(DL_DIALOGUE_DATA line)
        {
            for (int i = 0; i < line.segments.Count; i++)
            {
                DL_DIALOGUE_DATA.DIALOGUE_SEGMENT segment = line.segments[i];

                yield return WaitForDialogueSegmentSignalToBeTriggered(segment);

                yield return BuildDialogue(segment.dialogue, segment.appendText);
            }
        }

        public bool isWaitingAutoOnTimer { get; private set; } = false;

        IEnumerator WaitForDialogueSegmentSignalToBeTriggered(DL_DIALOGUE_DATA.DIALOGUE_SEGMENT segment)
        {
            switch(segment.startSignal)
            {
                case DL_DIALOGUE_DATA.DIALOGUE_SEGMENT. StartSignal.C:
                case DL_DIALOGUE_DATA.DIALOGUE_SEGMENT.StartSignal.A:
                    yield return WaitForUserInput();
                    break;
                case DL_DIALOGUE_DATA.DIALOGUE_SEGMENT.StartSignal.WC:
                case DL_DIALOGUE_DATA.DIALOGUE_SEGMENT.StartSignal.WA:
                    isWaitingAutoOnTimer = true;
                    yield return new WaitForSeconds(segment.signalDelay);
                    isWaitingAutoOnTimer = false;
                    break;
                default:
                    break;
            }

        }

        IEnumerator Line_RunDialogue(DIALOGUE_LINE line)
        {
            // show or hide speaker name if there is one present
            if (line.hasSpeaker)
                HandleSpeakerLogic(line.speakerData);

            //if the dialogue box is not visible - make sure it becomes automatically
            if (!dialogueSystem.dialogueContainer.isVisible)
                dialogueSystem.dialogueContainer.Show();

            //Build dialogue
            yield return BuildLineSegments(line.dialogueData);
        }

        IEnumerator BuildDialogue(string dialogue, bool append = false)
        {
            dialogue = tagManager.Inject(dialogue);

            //build dialogue
            if (!append)
                architect.Build(dialogue);
            else
                architect.Append(dialogue);

            //wait for dialogue to finish
            while (architect.isBuilding)
            {
                //ETONG IF STATEMENT UNG NAKALIMUTAN KO GRRRRRRF;LKFKDS;
                if (userPrompt)
                {
                    //EO ETO UNG SUSPEK
                    if (!architect.hurryUp)
                        architect.hurryUp = true;
                    else
                        architect.ForceComplete();

                    userPrompt = false;
                }
                yield return null;
            }
        }

        IEnumerator WaitForUserInput()
        {
            dialogueSystem.prompt.Show();

            while (!userPrompt)
                yield return null;

            dialogueSystem.prompt.Hide();

            userPrompt = false;

        }
    }
}
