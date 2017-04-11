using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceyOS
{
    class SpaceyOS
    {
        bool _exit = false;
        public bool Exit { get { return _exit; } private set { } }

        DirectoryInfo rootDirectory;

        DirectoryInfo workingDirectory;
        public string WorkingDirectory
        {
            get
            {
                var name = workingDirectory.FullName;
                var index = name.IndexOf(rootDirectory.FullName) + rootDirectory.FullName.Length;
                var ret = name.Substring(index);
                if (ret.Length == 0)
                    ret = @"\";
                ret = ret.Replace('\\', '/');
                return ret;
            }
            private set { }
        }

        void Init(string rootFolder)
        {
            if (!Directory.Exists(rootFolder))
                Directory.CreateDirectory(rootFolder);
            workingDirectory = rootDirectory = new DirectoryInfo(rootFolder);
        }

        public SpaceyOS()
        {
            Init("SpaceyOS");
        }

        public SpaceyOS(string rootFolder)
        {
            Init(rootFolder);
        }

        public TerminalLine[] ReadLine(string line)
        {
            if (line == "exit")
            {
                _exit = true;
                return new TerminalLine[] { };
            }

            if (!SpaceyCommand.TryParse(line, out SpaceyCommand command))
            {
                return new TerminalLine[] { new TerminalLine("invalid command") };
            }
            else if (command.Command == "ls")
            {
                var path = workingDirectory.FullName;

                if (command.Parameters.Count >= 1)
                    path = GetFullPath(command.Parameters[0]);

                if (!Directory.Exists(path))
                    return new TerminalLine[] { new TerminalLine("directory does not exist", ConsoleColor.Red) };
                try
                {
                    var filesAndFolders = Directory.GetFileSystemEntries(path).Select(ff =>
                    {
                        var filename = Path.GetFileName(ff);
                        if (File.Exists(ff))
                            return new TerminalLine(filename, ConsoleColor.White);
                        else if (Directory.Exists(ff))
                            return new TerminalLine(filename, ConsoleColor.Cyan);
                        return new TerminalLine(filename);
                    }).ToArray();
                    return filesAndFolders;

                }
                catch (Exception e)
                {
                    return HandleIoError(e);
                }
            }
            else if (command.Command == "mkdir")
            {
                if (command.Parameters.Count != 1)
                    return new TerminalLine[] { new TerminalLine("invalid number of operands", ConsoleColor.Red) };

                Directory.CreateDirectory(Path.Combine(workingDirectory.FullName, command.Parameters[0]));

            }
            else if (command.Command == "rmdir")
            {
                if (command.Parameters.Count != 1)
                    return new TerminalLine[] { new TerminalLine("invalid number of operands", ConsoleColor.Red) };

                var target = GetFullPath(command.Parameters[0]);
                var recursive = command.Flags.Contains("-r");

                if (!Directory.Exists(target))
                    return new TerminalLine[] { new TerminalLine("directory does not exist", ConsoleColor.Red) };
                if (Directory.EnumerateFileSystemEntries(target).Count() > 0 && !recursive)
                    return new TerminalLine[] { new TerminalLine("directory is not empty, use -r flag", ConsoleColor.Red) };
                try
                {
                    Directory.Delete(target, recursive);
                }
                catch (IOException e)
                {
                    return HandleIoError(e);
                }

            }
            else if (command.Command == "cd")
            {
                if (command.Parameters.Count != 1)
                    return new TerminalLine[] { new TerminalLine("invalid number of operands", ConsoleColor.Red) };

                var target = workingDirectory;

                if (command.Parameters[0] == "..")
                {
                    if (workingDirectory.FullName != rootDirectory.FullName)
                        workingDirectory = workingDirectory.Parent;
                }
                else
                    workingDirectory = new DirectoryInfo(GetFullPath(command.Parameters[0]));

            }
            else if (command.Command == "cp")
            {
                if (command.Parameters.Count != 2)
                    return new TerminalLine[] { new TerminalLine("invalid number of operands", ConsoleColor.Red) };

                var file1 = GetFullPath(command.Parameters[0]);
                var file2 = GetFullPath(command.Parameters[1]);
                var overwrite = command.Flags.Contains("-o");

                if (!File.Exists(file1))
                    return new TerminalLine[] { new TerminalLine("source file does not exist", ConsoleColor.Red) };
                if (File.Exists(file2) && !overwrite)
                    return new TerminalLine[] { new TerminalLine("destination file already exists, use -o to overwrite", ConsoleColor.Red) };

                try
                {
                    File.Copy(file1, file2, overwrite);
                }
                catch (Exception e)
                {
                    return HandleIoError(e);
                }
            }
            else if (command.Command == "mv")
            {
                if (command.Parameters.Count != 2)
                    return new TerminalLine[] { new TerminalLine("invalid number of operands", ConsoleColor.Red) };

                var file1 = GetFullPath(command.Parameters[0]);
                var file2 = GetFullPath(command.Parameters[1]);
                var overwrite = command.Flags.Contains("-o");

                if (!File.Exists(file1))
                    return new TerminalLine[] { new TerminalLine("source file does not exist", ConsoleColor.Red) };
                if (File.Exists(file2) && !overwrite)
                    return new TerminalLine[] { new TerminalLine("destination file already exists, use -o to overwrite", ConsoleColor.Red) };

                try
                {
                    if (File.Exists(file2) && overwrite)
                        File.Delete(file2);
                    File.Move(file1, file2);
                }
                catch (Exception e)
                {
                    return HandleIoError(e);
                }
            }
            else if (command.Command == "rm")
            {
                if (command.Parameters.Count != 1)
                    return new TerminalLine[] { new TerminalLine("invalid number of operands", ConsoleColor.Red) };

                var file = GetFullPath(command.Parameters[0]);
                if (!File.Exists(file))
                    return new TerminalLine[] { new TerminalLine("file does not exist", ConsoleColor.Red) };

                try
                {
                    File.Delete(file);
                }
                catch (Exception e)
                {
                    return HandleIoError(e);
                }
            }
            else if (command.Command == "touch")
            {
                if (command.Parameters.Count != 1)
                    return new TerminalLine[] { new TerminalLine("invalid number of operands", ConsoleColor.Red) };

                var file = GetFullPath(command.Parameters[0]);
                var overwrite = command.Flags.Contains("-o");
                if (File.Exists(file) && !overwrite)
                    return new TerminalLine[] { new TerminalLine("file already exists, use -o to overvrite", ConsoleColor.Red) };

                try
                {
                    File.Create(file).Close();
                }
                catch (Exception e)
                {
                    return HandleIoError(e);
                }
            }
            else if (command.Command == "nano")
            {
                if (command.Parameters.Count != 1)
                    return new TerminalLine[] { new TerminalLine("invalid number of operands", ConsoleColor.Red) };

                var file = GetFullPath(command.Parameters[0]);
                if (!File.Exists(file))
                    return new TerminalLine[] { new TerminalLine("file already exists, use -o to overvrite", ConsoleColor.Red) };

                Process.Start(file);

            }
            else if (command.Command == "compile")
            {
                var sources = workingDirectory.GetFiles("*.csx", SearchOption.AllDirectories).Select(f => f.FullName);
                AssemblyCompiler compiler = new AssemblyCompiler(sources.ToArray());
            }
            else
                return new TerminalLine[] { new TerminalLine("command not find", ConsoleColor.Red) };
            return new TerminalLine[0];
        }

        string GetFullPath(string path)
        {
            if (path.StartsWith("/"))
                return Path.Combine(rootDirectory.FullName, path.Substring(1));
            else
                return Path.Combine(workingDirectory.FullName, path);
        }

        TerminalLine[] HandleIoError(Exception e)
        {
            return new TerminalLine[] {
                new TerminalLine("operation failed", ConsoleColor.Red),
                new TerminalLine(e.Message.Replace(rootDirectory.FullName, "").Replace(@"\", "/"), ConsoleColor.Red) };
        }
    }

    class SpaceyCommand
    {
        public string Command { get; set; }
        public List<string> Flags { get; set; } = new List<string>();
        public List<string> Parameters { get; set; } = new List<string>();

        //TODO Accept paths with spaces, same as cmd
        public static bool TryParse(string line, out SpaceyCommand command)
        {
            command = new SpaceyCommand();
            line = line.Trim();
            var firstSpace = line.IndexOf(' ');
            if (firstSpace < 0)
                firstSpace = line.Length;


            var parts = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).AsEnumerable();

            command.Command = parts.FirstOrDefault();
            if (command.Command == null)
                return false;
            parts = parts.Skip(1);

            command.Flags = parts.Where(f => f.Trim().StartsWith("-")).ToList();
            command.Parameters = parts.Where(f => !f.Trim().StartsWith("-")).ToList();

            return true;
        }
    }

    class TerminalLine
    {
        public string Text { get; set; }
        public ConsoleColor BackgroundColor { get; set; } = ConsoleColor.Black;
        public ConsoleColor ForegroundColor { get; set; } = ConsoleColor.Green;
        public bool OverwriteColour { get; private set; } = false;

        public TerminalLine(string text)
        {
            Text = text;
        }
        public TerminalLine(string text, ConsoleColor foregroundColor)
        {
            Text = text;
            ForegroundColor = foregroundColor;
            OverwriteColour = true;
        }
        public TerminalLine(string text, ConsoleColor foregroundColor, ConsoleColor backgroundColor)
        {
            Text = text;
            ForegroundColor = foregroundColor;
            BackgroundColor = backgroundColor;
            OverwriteColour = true;
        }

    }
}
