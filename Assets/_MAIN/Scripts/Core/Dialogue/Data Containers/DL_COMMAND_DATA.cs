using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace DIALOGUE
{
    public class DL_COMMAND_DATA
    {
        public List<Command> commands;
        private const char COMMANDSPLITTER_ID = ',';
        private const char ARGUMENTSCONTAINERR_ID = '(';
        private const string WAITCOMMAND_ID = "[wait]";

        public struct Command
        {
            public string name;
            public string[] arguments;
            public bool waitForCompletion;
        }

        public DL_COMMAND_DATA(string rawCommands)
        {
            commands = RipCommands(rawCommands);
        }

        private List<Command> RipCommands(string rawCommands)
        {
            string[] data = rawCommands.Split(COMMANDSPLITTER_ID, System.StringSplitOptions.RemoveEmptyEntries);
            List<Command> result = new List<Command>();

            foreach (string cmd in data)
            {
                Command command = new Command();
                int index = cmd.IndexOf(ARGUMENTSCONTAINERR_ID);

                if (index == -1)
                {
                    command.name = cmd.Trim(); // No arguments, just use the whole command
                    command.arguments = new string[0]; // No arguments
                }
                else
                {
                    command.name = cmd.Substring(0, index).Trim();

                    // Prevent ArgumentOutOfRangeException if the command is incomplete
                    if (index + 1 < cmd.Length - 1)
                    {
                        command.arguments = GetArgs(cmd.Substring(index + 1, cmd.Length - index - 2));
                    }
                    else
                    {
                        command.arguments = new string[0]; // Ensure no errors on malformed input
                    }
                }

                if (command.name.ToLower().StartsWith(WAITCOMMAND_ID))
                {
                    command.name = command.name.Substring(WAITCOMMAND_ID.Length);
                    command.waitForCompletion = true;
                }

                result.Add(command);
            }

            return result;
        }

        private string[] GetArgs(string args)
        {
            List<string> argList = new List<string>();
            StringBuilder currentArg = new StringBuilder();
            bool inQuotes = false;

            for (int i = 0; i < args.Length; i++)
            {
                if (args[i] == '"')
                {
                    inQuotes = !inQuotes;
                    continue;
                }

                if (!inQuotes && args[i] == ' ')
                {
                    argList.Add(currentArg.ToString());
                    currentArg.Clear();
                    continue;
                }

                currentArg.Append(args[i]);
            }

            if (currentArg.Length > 0)
                argList.Add(currentArg.ToString());

            return argList.ToArray();
        }
    }
}
