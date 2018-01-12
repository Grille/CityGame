using System;
namespace GGL
{

    static public partial class Pharse
    {
        static public string[] ReplaceList = new string[0];
        static private string[] loadAndPrepare(string pfad)
        {
            string data;
            string[] lines;


            //string h = "dgfh"
            //    h.
            data = System.IO.File.ReadAllText(pfad);
            data = data.Replace('\n', '\x13'); //Normalize line ends
            data = data.Replace('\r', '\x13');
            data = data.Replace('\x9', '\x20');//Replace TAB with space
            data = data.Replace("{", ";{;");
            data = data.Replace("}", ";};");
            data = data.Replace("inf+", "2147483647");
            data = data.Replace("inf-", "-2147483647");
            for (int i = 0; i < ReplaceList.Length; i += 2) data = data.Replace(ReplaceList[i], ReplaceList[i + 1]);
            //data = data.Replace("true", "1");
            //data = data.Replace("false", "0");
            lines = data.Split(new char[] { '\x3b' });//split by ;

            for (int i = 0; i < lines.Length; i++)
            {
                //Remote comments
                if (lines[i].Contains("//"))
                {
                    string[] array = lines[i].Split(new char[] {'\x13'}, System.StringSplitOptions.None);
                    lines[i] = array[array.Length - 1];
                    if (lines[i].Contains("//")) lines[i] = "\x20";

                }
                lines[i] = lines[i].Replace('\x13', '\x20');
                lines[i] = lines[i].Trim('\x20');//remove space
            }
            return lines;
        }
        static private string[,] parseExample(int curLine, string[] lines)
        {
            int scope = 0;
            int amount = 0;
            int[] pos = new int[100];
            while (true)
            {
                curLine++;
                if (lines[curLine] == "{") scope += 1;
                else if (lines[curLine] == "}")
                {
                    scope -= 1;
                    if (scope == 0) break;
                }
                else if (lines[curLine].Contains("=") == true) pos[amount++] = curLine;

            }

            string[,] result = new string[amount, 3];
            for (int i = 0; i < amount; i++)
            {
                string comand = ((lines[pos[i]].Split('=')[0]).Trim('\x20')).ToLowerInvariant();
                result[i, 0] = (comand.Split('\x20')[0]).Trim('\x20');//type
                result[i, 1] = (comand.Split('\x20')[1]).Trim('\x20');//name
                result[i, 2] = (lines[pos[i]].Split('=')[1]).Trim('\x20');//value
            }

            return result;
        }
        static private object convertType(string typ, string value)
        {
            if /**/ (typ == "int") return Convert.ToInt32(value);
            else if (typ == "bool") return Convert.ToBoolean(value);
            else if (typ == "double") return Convert.ToDouble(value);
            else if (typ == "float") return (float)Convert.ToDouble(value);
            else if (typ == "string") return value.Trim('\x22');
            else if (typ == "int[]")
            {
                string[] line = value.Split(new char[] { '[', ',', ']' }, StringSplitOptions.RemoveEmptyEntries);
                int[] result = new int[line.Length];
                for (int i = 0; i < line.Length; i++) result[i] = Convert.ToInt32(line[i]);
                return result;
            }
            else if (typ == "double[]")
            {
                string[] line = value.Split(new char[] { '[', ',', ']' }, StringSplitOptions.RemoveEmptyEntries);
                double[] result = new double[line.Length];
                for (int i = 0; i < line.Length; i++) result[i] = Convert.ToDouble(line[i]);
                return result;
            }
            else if (typ == "int[,]")
            {
                value = value.Replace("[", ",[,");
                value = value.Replace("]", ",],");
                string[] line = value.Split(new char[] { ',' });
                int size = 0;
                for (int i = 0; i < line.Length-4; i++) if (line[i + 2].Contains("["))  size++;
                if (size == 0) return new int[0, 0];
                int otherSize = (line.Length - 4-size*4)/ size;
                int[,] result = new int[size, otherSize];
                int scope = 0;
                int pos = 0;
                for (int curLine = 0; curLine < line.Length; curLine++)
                {
                    if (line[curLine] == "[")
                    {
                        scope += 1;
                        if (scope == 2)
                        {
                            for (int i = 0; i < otherSize; i++) result[pos, i] = Convert.ToInt32(line[curLine + i + 1]);
                            pos++;
                        }
                    }
                    else if (line[curLine] == "]")
                    {
                        scope -= 1;
                        if (scope == 0) break;
                    }

                }
                return result;
            }
            return "error";
        }
        static public object[,] Load(string pfad)
        {
            int scope = 0, curObject = 0;
            string comand, input;
            string[] line = loadAndPrepare(pfad);
            int objects = 0;

            string[,] template = new string[0, 3];
            for (int i = 0; i < line.Length; i++)
            {
                comand = ((line[i].Split('=')[0]).Trim('\x20')).ToLowerInvariant();
                if (comand == "id") {
                    objects++;
                }
                else if (line[i].Trim('\x20') == "Template") template = parseExample(i, line);
            }
            int objectSize = template.GetLength(0);
            object[,] result = new object[objects, objectSize];

            for (int curLine = 0; curLine < line.Length; curLine++)
            {
                if (line[curLine] == "{")
                {
                    scope += 1;
                    if (scope == 2)
                        for (int i = 0; i < objectSize; i++)
                            result[curObject, i] = convertType(template[i, 0], template[i, 2]);//values[i];
                }
                else if (line[curLine] == "}")
                {
                    scope -= 1;
                    if (scope == 0) break;
                }
                else
                {
                    if (scope == 1 && (line[curLine].Split('=')[0]).Trim('\x20') == "ID") curObject = Convert.ToInt32(line[curLine].Split('=')[1].Trim('\x20'));
                    else if (scope == 2 && line[curLine].Contains("=") == true)
                    {
                        comand = ((line[curLine].Split('=')[0]).Trim('\x20')).ToLowerInvariant();
                        input = (line[curLine].Split('=')[1]).Trim('\x20');
                        for (int i = 0; i < objectSize; i++)
                        {
                            if (comand == template[i, 1])
                            {
                                //Console.WriteLine("curO=" + curObject);
                                //Console.WriteLine("curI=" + i);
                                //Console.WriteLine("Comand=" + comand);
                                //Console.WriteLine("Input=" + input);
                                //Console.WriteLine("------------------------------");

                                result[curObject, i] = convertType(template[i, 0],input);

                                i = objectSize;
                            }
                        }
                    }
                }
            }//while (Loop == true)
            return result;
        }
    }
}