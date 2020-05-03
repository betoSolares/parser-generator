using System.IO;

namespace SolutionGenerator
{
    public class Basic
    {
        /// <summary>Write all the basic files</summary>
        /// <param name="name">The name of the solution</param>
        /// <param name="path">The path for the files</param>
        public void WriteFiles(string name, string path)
        {
            WriteSln(name, path);
            WriteProject(name, Path.Combine(path, name));
            WriteMainProgram(name, Path.Combine(path, name));
            WriteAppConfiguration(Path.Combine(path, name));
        }

        /// <summary>Write the application configuration file</summary>
        /// <param name="path">The path for the file</param>
        private void WriteAppConfiguration(string path)
        {
            string text = "<?xml version=\"1.0\" encoding=\"utf-8\" ?>\n<configuration>\n\t<startup> \n";
            text += "\t\t<supportedRuntime version=\"v4.0\" sku=\".NETFramework,Version = v4.6\" />\n";
            text += "\t</startup>\n</configuration>";
            File.WriteAllText(Path.Combine(path, "App.config"), text);
        }

        /// <summary>Write the main program</summary>
        /// <param name="name">The name of the solution</param>
        /// <param name="path">The path for the file</param>
        private void WriteMainProgram(string name, string path)
        {
            string text = "using System;\nusing System.Windows.Forms;\n\n namespace " + name +"\n{\n\tstatic class";
            text += " Program\n\t{\n\t\t/// <summary>The main entry point for the application.</summary>\n\t\t";
            text += "[STAThread]\n\t\tstatic void Main()\n\t\t{\n\t\t\tApplication.EnableVisualStyles();\n\t\t\t";
            text += "Application.SetCompatibleTextRenderingDefault(false);\n\t\t\t";
            text += "Application.Run(new UI.MainView());\n\t\t}\n\t}\n}\n";
            File.WriteAllText(Path.Combine(path, "Program.cs"), text);
        }

        /// <summary>Write the project file</summary>
        /// <param name="name">The name of the solution</param>
        /// <param name="path">The path for the file</param>
        private void WriteProject(string name, string path)
        {
            string text = "<?xml version=\"1.0\" encoding=\"utf-8\"?>\n";
            text += "<Project ToolsVersion=\"15.0\" xmlns=\"http://schemas.microsoft.com/developer/msbuild/2003\">\n";
            text += "  <Import Project=\"$(MSBuildExtensionsPath)\\$(MSBuildToolsVersion)\\Microsoft.Common.props\"";
            text += " Condition=\"Exists('$(MSBuildExtensionsPath)\\$(MSBuildToolsVersion)\\Microsoft.Common.props')";
            text += "\" />\n  <PropertyGroup>\n    <Configuration Condition=\" '$(Configuration)' == '' \">Debug";
            text += "</Configuration>\n    <Platform Condition=\" '$(Platform)' == '' \">AnyCPU</Platform>\n";
            text += "    <ProjectGuid>{93E6F700-FD90-42F7-AC22-326FAC929F8B}</ProjectGuid>\n    <OutputType>";
            text += "WinExe</OutputType>\n    <RootNamespace>" + name + "</RootNamespace>\n    <AssemblyName>";
            text += name + "</AssemblyName>\n    <TargetFrameworkVersion>v4.6</TargetFrameworkVersion>\n    ";
            text += "<FileAlignment>512</FileAlignment>\n    <AutoGenerateBindingRedirects>true";
            text += "</AutoGenerateBindingRedirects>\n    <Deterministic>true</Deterministic>\n  </PropertyGroup>\n  ";
            text += "<PropertyGroup Condition=\" '$(Configuration)|$(Platform)' == 'Debug|AnyCPU' \">\n    ";
            text += "<PlatformTarget>AnyCPU</PlatformTarget>\n    <DebugSymbols>true</DebugSymbols>\n";
            text += "    <DebugType>full</DebugType>\n    <Optimize>false</Optimize>\n    <OutputPath>bin\\Debug\\";
            text += "</OutputPath>\n    <DefineConstants>DEBUG;TRACE</DefineConstants>\n    <ErrorReport>prompt";
            text += "</ErrorReport>\n    <WarningLevel>4</WarningLevel>\n  </PropertyGroup>\n  ";
            text += "<PropertyGroup Condition=\" '$(Configuration)|$(Platform)' == 'Release|AnyCPU' \">\n    ";
            text += "<PlatformTarget>AnyCPU</PlatformTarget>\n    <DebugType>pdbonly</DebugType>\n    <Optimize>true";
            text += "</Optimize>\n    <OutputPath>bin\\Release\\</OutputPath>\n    <DefineConstants>TRACE";
            text += "</DefineConstants>\n    <ErrorReport>prompt</ErrorReport>\n    <WarningLevel>4</WarningLevel>\n";
            text += "  </PropertyGroup>\n  <ItemGroup>\n    <Reference Include=\"System\" />\n    <Reference Include=";
            text += "\"System.Core\" />\n    <Reference Include=\"System.Xml.Linq\" />\n    <Reference Include=\"";
            text += "System.Data.DataSetExtensions\" /> \n    <Reference Include=\"Microsoft.CSharp\" />\n    ";
            text += "<Reference Include=\"System.Data\" />\n    <Reference Include=\"System.Deployment\" />\n    ";
            text += "<Reference Include=\"System.Drawing\" />\n    <Reference Include=\"System.Net.Http\" />\n    ";
            text += "<Reference Include=\"System.Windows.Forms\" />\n    <Reference Include=\"System.Xml\" />\n  ";
            text += "</ItemGroup>\n  <ItemGroup>\n    <Compile Include=\"Helpers\\Evaluator.cs\" />\n    ";
            text += "<Compile Include=\"Helpers\\Tokenizer.cs\" />\n    <Compile Include=\"Program.cs\" />\n    ";
            text += "<Compile Include=\"Properties\\AssemblyInfo.cs\" />\n    <Compile Include=\"UI\\Lexeme\\";
            text += "LexemeView.cs \">\n      <SubType>Form</SubType>\n    </Compile>\n    <Compile Include=";
            text += "\"UI\\Lexeme\\LexemeView.Designer.cs\">\n      <DependentUpon>LexemeView.cs</DependentUpon>\n";
            text += "    </Compile>\n    <Compile Include=\"UI\\Main\\MainView.cs\">\n      <SubType>Form</SubType>\n";
            text += "    </Compile>\n<Compile Include=\"UI\\Main\\MainView.Designer.cs\">\n<DependentUpon>MainView.cs";
            text += "</DependentUpon>\n    </Compile>\n    <EmbeddedResource Include=\"Properties\\Resources.resx\">\n";
            text += "      <Generator>ResXFileCodeGenerator</Generator>\n      <LastGenOutput>Resources.Designer.cs";
            text += "</LastGenOutput>\n      <SubType>Designer</SubType>\n    </EmbeddedResource>\n    <Compile ";
            text += "Include=\"Properties\\Resources.Designer.cs\">\n      <AutoGen>True</AutoGen>\n      ";
            text += "<DependentUpon>Resources.resx</DependentUpon>\n    </Compile>\n    <EmbeddedResource Include=\"";
            text += "UI\\Lexeme\\LexemeView.resx\">\n      <DependentUpon>LexemeView.cs</DependentUpon>\n    ";
            text += "</EmbeddedResource>\n    <EmbeddedResource Include=\"UI\\Main\\MainView.resx\">\n      ";
            text += "<DependentUpon>MainView.cs</DependentUpon>\n    </EmbeddedResource>\n    <None Include=\"";
            text += "Properties\\Settings.settings\">\n      <Generator>SettingsSingleFileGenerator</Generator>\n";
            text += "      <LastGenOutput>Settings.Designer.cs</LastGenOutput>\n    </None>\n    <Compile Include=\"";
            text += "Properties\\Settings.Designer.cs\">\n      <AutoGen>True</AutoGen>\n      <DependentUpon>";
            text += "Settings.settings</DependentUpon>\n      <DesignTimeSharedInput>True</DesignTimeSharedInput>\n";
            text += "    </Compile>\n  </ItemGroup>\n  <ItemGroup>\n    <None Include=\"App.config\" />\n  ";
            text += "  </ItemGroup>\n  <Import Project=\"$(MSBuildToolsPath)\\Microsoft.CSharp.targets\" />\n";
            text += "</Project>";
            File.WriteAllText(Path.Combine(path, name + ".csproj"), text);
        }

        /// <summary>Write the sln file</summary>
        /// <param name="name">The name of the solution</param>
        /// <param name="path">The path for the file</param>
        private void WriteSln(string name, string path)
        {
            string text = "\nMicrosoft Visual Studio Solution File, Format Version 12.00\n";
            text += "# Visual Studio Version 16\nVisualStudioVersion = 16.0.29411.108\n";
            text += "MinimumVisualStudioVersion = 10.0.40219.1\nProject(\"{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}\"";
            text += ") = \"" + name + "\", \"" + name + "\\" + name + ".csproj\", \"";
            text += "{93E6F700-FD90-42F7-AC22-326FAC929F8B}\"\nEndProject\nGlobal\n";
            text += "\tGlobalSection(SolutionConfigurationPlatforms) = preSolution\n\t\t";
            text += "Debug|Any CPU = Debug|Any CPU\n\t\tRelease|Any CPU = Release|Any CPU\n\tEndGlobalSection\n";
            text += "\tGlobalSection(ProjectConfigurationPlatforms) = postSolution\n";
            text += "\t\t{93E6F700-FD90-42F7-AC22-326FAC929F8B}.Debug|Any CPU.ActiveCfg = Debug|Any CPU\n";
            text += "\t\t{93E6F700-FD90-42F7-AC22-326FAC929F8B}.Debug|Any CPU.Build.0 = Debug|Any CPU\n";
            text += "\t\t{93E6F700-FD90-42F7-AC22-326FAC929F8B}.Release|Any CPU.ActiveCfg = Release|Any CPU\n";
            text += "\t\t{93E6F700-FD90-42F7-AC22-326FAC929F8B}.Release|Any CPU.Build.0 = Release|Any CPU\n";
            text += "\tEndGlobalSection\n\tGlobalSection(SolutionProperties) = preSolution\n";
            text += "\t\tHideSolutionNode = FALSE\n\tEndGlobalSection\n";
            text += "\tGlobalSection(ExtensibilityGlobals) = postSolution\n";
            text += "\t\tSolutionGuid = {0A812593-3D4E-49FE-BC23-F6F21D414D02}\n";
            text += "\tEndGlobalSection\nEndGlobal";
            File.WriteAllText(Path.Combine(path, name + ".sln"), text);
        }
    }
}
