using System;
using System.Collections.Generic;
using System.IO;

namespace SolutionGenerator
{
    public class Generator
    {
        private readonly string AppName;
        private readonly string MainPath;
        private readonly Basic basicWriter = new Basic();
        private readonly Properties propertiesWriter = new Properties();
        private readonly MainUI mainUIWriter = new MainUI();
        private readonly LexemeUI lexemeUIWriter = new LexemeUI();
        private readonly Helpers helpersWriter = new Helpers();
        private readonly Dictionary<string, string> tokens;
        private readonly Dictionary<string, string> actions;
        private readonly Dictionary<string, string> sets;
        private readonly Dictionary<Tuple<string, List<int>, bool>, Dictionary<string, List<int>>> transitions;

        /// <summary>Constructor</summary>
        /// <param name="name">The name of the solution</param>
        /// <param name="path">The path for the solution</param>
        /// <param name="tokens">The tokens of the text</param>
        /// <param name="sets">The sets of the text</param>
        /// <param name="actions">The actions of the text</param>
        /// <param name="transitions">The satate machine generated</param>
        public Generator(string name,
                         string path,
                         Dictionary<string, string> tokens,
                         Dictionary<string, string> actions,
                         Dictionary<string, string> sets,
                         Dictionary<Tuple<string, List<int>, bool>,
                         Dictionary<string, List<int>>> transitions)
        {
            AppName = name;
            MainPath = Path.Combine(path, name);
            this.tokens = tokens;
            this.actions = actions;
            this.sets = sets;
            this.transitions = transitions;
        }

        /// <summary>Generate the solution</summary>
        public void GenerateSolution()
        {
            Directory.CreateDirectory(MainPath);
            Directory.CreateDirectory(Path.Combine(MainPath, AppName));
            basicWriter.WriteFiles(AppName, MainPath);
            Directory.CreateDirectory(Path.Combine(MainPath, AppName, "Properties"));
            propertiesWriter.WriteFiles(AppName, Path.Combine(MainPath, AppName, "Properties"));
            Directory.CreateDirectory(Path.Combine(MainPath, AppName, "UI"));
            Directory.CreateDirectory(Path.Combine(MainPath, AppName, "UI", "Main"));
            Directory.CreateDirectory(Path.Combine(MainPath, AppName, "UI", "Lexeme"));
            mainUIWriter.WriteFiles(AppName, Path.Combine(MainPath, AppName, "UI", "Main"));
            lexemeUIWriter.WriteFiles(AppName, Path.Combine(MainPath, AppName, "UI", "Lexeme"));
            Directory.CreateDirectory(Path.Combine(MainPath, AppName, "Helpers"));
            helpersWriter.WriteFiles(AppName, Path.Combine(MainPath, AppName, "Helpers"), tokens, actions, sets,
                                     transitions);
        }
    }
}
