using System;
using System.CodeDom.Compiler;
using System.IO;
using System.Reflection;

namespace CityGame
{
    public partial class Game
    {
        void compile()
        {
            try
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Gray;

                var codeProvider = CodeDomProvider.CreateProvider("CSharp");
                var compiler = codeProvider.CreateCompiler();
                var parameter = new CompilerParameters();

                parameter.ReferencedAssemblies.Add("system.dll");
                parameter.ReferencedAssemblies.Add(AppDomain.CurrentDomain.FriendlyName);
                parameter.CompilerOptions = "/t:library";
                parameter.GenerateInMemory = true;

                string code = generateCode();

                var result = compiler.CompileAssemblyFromSource(parameter, code);
                if (result.Errors.Count > 0)
                {
                    Console.Error.WriteLine("LINE: " + result.Errors[0].Line + " ERROR: " + result.Errors[0].ErrorText);
                }
                Assembly assembly = result.CompiledAssembly;
                //object program = assembly.CreateInstance("ProLernParser.Program");

                Type type = assembly.GetType("ScriptEnv.ScriptEnv");
                for (int i = 0; i < Objects.Length; i++)
                {
                    MethodInfo onupdateInfo = type.GetMethod(Objects[i].Name+"_onupdate");
                    if (onupdateInfo != null)
                    {
                        Action onupdate = (Action)Delegate.CreateDelegate(typeof(Action), onupdateInfo);
                        Objects[i].OnUpdate = onupdate;
                    }
                }
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e.Message);
            }
        }
        string generateCode()
        {
            string code = "";
            code += "namespace ScriptEnv{\n";

            code += "public static class objects {\n";
            code += "public static CityGame.GameObject " + Objects[0].Name;
            for (int i = 1; i < Objects.Length; i++)
                code += "," + Objects[i].Name;
            code += ";\n";
            code += "public static void Init(CityGame.GameObject[] _Ary){\n";
            for (int i = 0; i < Objects.Length; i++)
                code += Objects[i].Name + "=_Ary[" + i + "];";
            code += "\n}}\n";

            code += "public static class resources {\n";
            code += "public static CityGame.GameResources " + Resources[0].Name;
            for (int i = 1; i < Resources.Length; i++)
                code += "," + Resources[i].Name;
            code += ";\n";
            code += "public static void Init(CityGame.GameResources[] _Ary){\n";
            for (int i = 0; i < Resources.Length; i++)
                code += Resources[i].Name + "=_Ary[" + i + "];";
            code += "\n}}\n";

            code += "public static class ScriptEnv {\n";

            for (int i = 0; i < Objects.Length; i++)
            {
                GameObject obj = Objects[i];
                if (obj.SrcOnUpdate != null)
                {
                    code += "public static void " + obj.Name + "_onupdate(){\n";
                    code += obj.SrcOnUpdate;
                    code += "}\n";
                }
            }
            code += "}}\n";

            Console.WriteLine(code);
            return code;
        }
    }
}