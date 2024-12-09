using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Lumin
{
    public static class helpfunc
    {
      public  static string WrapWordsInBrackets(string input)
        {
            string pattern = @"\b[a-zA-Z][\w\.]*@?\b";

            // Замена найденных слов на обернутые в []
            string result = Regex.Replace(input, pattern, m => $"[{m.Value}]");
            result = result.Replace("]@", "@]");
            return result;
        }
        public static string RemoveAtSymbols(string input)
        {
            StringBuilder sb = new StringBuilder();
            bool insideSingleQuote = false;
            bool insideDoubleQuote = false;

            for (int i = 0; i < input.Length; i++)
            {
                char currentChar = input[i];


                if (currentChar == '\'' && !insideDoubleQuote)
                {
                    insideSingleQuote = !insideSingleQuote;
                    sb.Append(currentChar);
                }
                else if (currentChar == '\"' && !insideSingleQuote)
                {
                    insideDoubleQuote = !insideDoubleQuote;
                    sb.Append(currentChar);
                }
                else if (currentChar == '@' && !insideSingleQuote && !insideDoubleQuote)
                {

                    continue;
                }
                else
                {
                    sb.Append(currentChar);
                }
            }

            return sb.ToString();
        }
        public static void ParsImportlum(string realcommandto, string file, List<string> func, List<string> peremen, List<string> typeperemen, List<string> macroses, List<string> struc)
        {
            string[] command3 = File.ReadAllLines(realcommandto.TrimStart());



            for (int i1 = 0; i1 < command3.Count(); i1++)
            {
                if (command3[i1].EndsWith(':'))
                {
                    func.Add(command3[i1].Replace(":", "").Trim());// Console.WriteLine(command3[i1].Replace(":", "").Trim());
                }
                else if (command3[i1].Contains(" db "))
                {
                    string[] a = command3[i1].TrimStart().Split("db");
                    peremen.Add(a[0].Trim());
                    typeperemen.Add("byte");
                }
                else if (command3[i1].Contains(" dw "))
                {
                    string[] a = command3[i1].TrimStart().Split("dw");
                    peremen.Add(a[0].Trim());
                    //  Console.WriteLine("rrrrrrrrrr");
                    typeperemen.Add("word");
                }
                else if (command3[i1].Contains(" dd "))
                {
                    string[] a = command3[i1].TrimStart().Split(" dd ");
                    peremen.Add(a[0].Trim());
                    typeperemen.Add("dword");
                }
                else if (command3[i1].Contains(" dq "))
                {
                    string[] a = command3[i1].TrimStart().Split(" dq ");
                    peremen.Add(a[0].Trim());
                    typeperemen.Add("qword");
                }
                else if (command3[i1].TrimStart().StartsWith("macro "))
                {

                    //  File.AppendAllText(file, $"\n {command3[i].Replace("dword ", null).Replace("word ", null).Replace("qword ", null).Replace("tword ", null).Replace("byte", null).Replace("(", " ").Replace(")", null)}");
                    string[] spl = command3[i1].TrimStart().Split("macro ", 2);
                    string[] macro = spl[1].Split(' ', 2);

                    macroses.Add($"{macro[0].Trim()}");

                    //  Console.WriteLine($"{macro[0].Trim()}" + "fdfsdfsdfsdfsggggggggggggggggggggggggggggggggggggggggggggggggggggggd");
                }
                else if (command3[i1].TrimStart().StartsWith("struc "))
                {

                    //  File.AppendAllText(file, $"\n {command3[i].Replace("dword ", null).Replace("word ", null).Replace("qword ", null).Replace("tword ", null).Replace("byte", null).Replace("(", " ").Replace(")", null)}");
                    string[] spl = command3[i1].TrimStart().Split("struc ", 2);


                    struc.Add($"{spl[1].Trim()}");

                    // Console.WriteLine($"{macro[0].Trim()}" + "fdfsdfsdfsdfsggggggggggggggggggggggggggggggggggggggggggggggggggggggd");
                }
                else if (command3[i1].TrimStart().StartsWith("proc "))
                {

                    //  File.AppendAllText(file, $"\n {command3[i].Replace("dword ", null).Replace("word ", null).Replace("qword ", null).Replace("tword ", null).Replace("byte", null).Replace("(", " ").Replace(")", null)}");
                    string[] spl = command3[i1].TrimStart().Split("proc ", 2);
                    string[] macro = spl[1].Split(' ', 2);

                    func.Add($"{macro[0].Trim()}");

                    //Console.WriteLine($"{macro[0].Trim()}" + "fdfsdfsdfsdfsggggggggggggggggggggggggggggggggggggggggggggggggggggggd");
                }
            }
        }

        public static string TrimOutsideQuotes(string command)
        {
            bool inSingleQuotes = false;
            bool inDoubleQuotes = false;

            int index = -1;

            for (int j = 0; j < command.Length; j++)
            {
                if (command[j] == '\'' && !inDoubleQuotes)
                {
                    inSingleQuotes = !inSingleQuotes;
                }
                else if (command[j] == '"' && !inSingleQuotes)
                {
                    inDoubleQuotes = !inDoubleQuotes;
                }
                else if (command[j] == '#' && !inSingleQuotes && !inDoubleQuotes)
                {
                    index = j;
                    break;
                }
            }

            if (index != -1)
            {
                return command.Substring(0, index);
            }
            return command;
        }
        public static void ProcessCommandWithSemicolon(string command, List<string> result)
        {
            bool inSingleQuotes = false;
            bool inDoubleQuotes = false;
            int lastSeparatorIndex = 0;

            for (int i = 0; i < command.Length; i++)
            {
                if (command[i] == '"' && !inSingleQuotes)
                {
                    inDoubleQuotes = !inDoubleQuotes; // Переключаем состояние
                }
                else if (command[i] == '\'' && !inDoubleQuotes)
                {
                    inSingleQuotes = !inSingleQuotes; // Переключаем состояние
                }
                else if (command[i] == ';' && !inSingleQuotes && !inDoubleQuotes)
                {
                    result.Add(command.Substring(lastSeparatorIndex, i - lastSeparatorIndex).Trim());
                    lastSeparatorIndex = i + 1; // Обновляем индекс
                }
            }


            if (lastSeparatorIndex < command.Length)
            {
                result.Add(command.Substring(lastSeparatorIndex).Trim());
            }
        }
        public static List<string> ParseCommands(string input)
        {
            List<string> commands = new List<string>();
            bool inSingleQuote = false;
            bool inDoubleQuote = false;
            int lastSeparatorIndex = 0;

            for (int i = 0; i < input.Length; i++)
            {
                char currentChar = input[i];

                // Проверка на кавычки
                if (currentChar == '\'' && !inDoubleQuote)
                    inSingleQuote = !inSingleQuote;
                else if (currentChar == '"' && !inSingleQuote)
                    inDoubleQuote = !inDoubleQuote;

                // Проверка на разделение по точкам с запятой
                if (currentChar == ';' && !inSingleQuote && !inDoubleQuote)
                {
                    commands.Add(input.Substring(lastSeparatorIndex, i - lastSeparatorIndex).Trim());
                    lastSeparatorIndex = i + 1;
                }
            }

            // Добавляем оставшуюся часть строки
            if (lastSeparatorIndex < input.Length)
            {
                commands.Add(input.Substring(lastSeparatorIndex).Trim());
            }

            return commands;
        }
        public static string parsstring(string wat, bool pointer,List<string> stringstoaddend,int stringlasted)
        {
            stringlasted++;
            if (wat.Trim().StartsWith("\"") && wat.Trim().EndsWith("\""))
            {

                foreach (var item in stringstoaddend)
                {
                    if (item.Contains(wat + ",0"))
                    {

                        return item.Substring(0, item.IndexOf(' ')) + "@";
                    }
                }
                stringstoaddend.Add($"bytedaddedLC{stringlasted} db {wat},0");
                if (pointer) return $"bytedaddedLC{stringlasted}@";

                return $"bytedaddedLC{stringlasted}";
            }
            return wat;
        }
        public static bool ismacro(string command, List<string> func, string file, string line, string pere)
        {


            for (int ia = 0; ia < func.Count; ia++)
            {
                bool ismacro = false;
                string com = command;
                string macro = null;
                if (com.TrimStart().Contains(" "))
                {
                    macro = com.TrimStart();
                    string[] e = macro.Split(' ', 2);
                    macro = e[0];
                    com = com;
                    ismacro = true;
                    com = macro;

                }
                if (!ismacro)
                {
                    com = command.Replace(" ", null);


                }
                else
                {
                    // Console.WriteLine(macro + " " + func[ia]);
                    if (com.TrimStart().StartsWith(func[ia]))
                    {
                        string[] h = line.TrimStart().Split(" ", 2);
                        string[] parts = h[1].TrimStart().Split(',');
                        int buffer = 0;

                        for (int i = 0; i < parts.Length; i++)
                        {
                            if (char.IsLetter(parts[i].TrimStart()[0]) && parts[i].Trim() != "''" && parts[i].Trim() != "\"\"" && !parts[i].Trim().StartsWith("'") && !parts[i].Trim().StartsWith("\""))
                            { parts[i] = $"[{parts[i]}]"; }

                        }


                        string g = string.Join(",", parts);
                        File.AppendAllText(file, "\n");
                        File.AppendAllText(file, "\n" + h[0] + " " + g + $"\nmov [{pere}],eax");
                        return true;
                        break;
                    }

                }

            }
            return false;

        }

        public static string RemoveBrackets(string input)//////////////////////////////
        {
            string line = input;


            string pattern = @"\[\s*(.*?)\s*(@)?\s*\]";


            string result = Regex.Replace(line, pattern, m =>
            m.Groups[2].Success ? m.Groups[1].Value : m.Value);
            return result;

        }
        public static List<string> SplitString(string input)
        {
            List<string> result = new List<string>();
            bool inSingleQuote = false;
            bool inDoubleQuote = false;
            string currentPart = "";
            bool inc = false;
            if (input.Contains(","))
            {
                for (int i = 0; i < input.Length; i++)
                {
                    char c = input[i];


                    if (c == '\'' && !inDoubleQuote)
                    {
                        inSingleQuote = !inSingleQuote;
                    }
                    else if (c == '\"' && !inSingleQuote)
                    {
                        inDoubleQuote = !inDoubleQuote;
                    }
                    else if (c == '(' && !inSingleQuote && !inDoubleQuote)
                    {
                        inc = true;
                    }
                    else if (c == ')' && !inSingleQuote && !inDoubleQuote && inc)
                    {
                        inc = false;
                    }
                    else if (c == ',' && !inSingleQuote && !inDoubleQuote && !inc)
                    {
                        result.Add(currentPart);
                        currentPart = "";
                        continue;
                    }

                    currentPart += c;
                }
            }
            else
            {
                result.Add((string)input);
                return result;
            }


            if (!string.IsNullOrWhiteSpace(currentPart))
            {
                result.Add(currentPart);
            }

            return result;
        }

        public static string RemoveExtraSpaces(string input)
        {
            StringBuilder result = new StringBuilder();
            bool insideQuotes = false;

            for (int i = 0; i < input.Length; i++)
            {
                char currentChar = input[i];

                // Проверяем, не является ли текущий символ кавычкой
                if (currentChar == '"' || currentChar == '\'')
                {
                    insideQuotes = !insideQuotes; // Меняем состояние внутри кавычек
                    result.Append(currentChar);
                }
                else if (char.IsWhiteSpace(currentChar))
                {
                    if (insideQuotes)
                    {
                        result.Append(currentChar); // Добавляем пробелы внутри кавычек
                    }
                    else if (result.Length > 0 && result[result.Length - 1] != ' ')
                    {
                        result.Append(' '); // Добавляем один пробел вне кавычек
                    }
                }
                else
                {
                    result.Append(currentChar); // Добавляем обычные символы
                }
            }

            // Убираем лишние пробелы в начале и конце строки
            return result.ToString().Trim();
        }
        public static string TrimOutsideQuotesprep(string command)
        {
            bool inSingleQuotes = false;
            bool inDoubleQuotes = false;

            int index = -1;

            for (int j = 0; j < command.Length; j++)
            {
                if (command[j] == '\'' && !inDoubleQuotes)
                {
                    inSingleQuotes = !inSingleQuotes;
                }
                else if (command[j] == '"' && !inSingleQuotes)
                {
                    inDoubleQuotes = !inDoubleQuotes;
                }
                else if (command[j] == '#' && !inSingleQuotes && !inDoubleQuotes)
                {
                    index = j;
                    break;
                }
            }

            if (index != -1)
            {
                return command.Substring(0, index);
            }
            return command;
        }
        public static string RemoveBracketsprep(string input)//////////////////////////////
        {
            string line = input;


            string pattern = @"\[\s*(.*?)\s*(@)?\s*\]";


            string result = Regex.Replace(line, pattern, m =>
            m.Groups[2].Success ? m.Groups[1].Value : m.Value);
            return result;

        }
        public static string Removecav(string input)
        {
            string output = input;
            string pattern = @"(?<!['""])(\S+)\[(\S+)\](?!['""])";
            if (input.IndexOf("[") != 0)
            {
                if (char.IsLetter(input[input.IndexOf("[") - 1]))
                {
                    output = Regex.Replace(input, pattern, "$1+$2");
                }

            }
            return output;
        }
        public static string RemovePd(string line)
        {
            string pattern = @"(?<![""'$])\[(.*?)\((.*?)\)\](?![""'$])";


            string output = Regex.Replace(line, pattern, m => $"{m.Groups[2].Value} {m.Groups[1].Value}");
            return output;
        }
        public static string ReplaceRegisters(string line)
        {
            // Шаблон для поиска 32-битных регистров, которые не находятся в скобках
            string pattern = @"\b(ebx|eax|ecx|edx|esi|edi|esp|ebp)\b(?!\s*\()"; // Изменено

            // Словарь для замены 32-битных регистров на 16-битные
            var replacements = new System.Collections.Generic.Dictionary<string, string>
        {
            { "eax", "ax" },
            { "ebx", "bx" },
            { "ecx", "cx" },
            { "edx", "dx" },
            { "esi", "si" },
            { "edi", "di" },
            { "esp", "sp" },
            { "ebp", "bp" }
        };

            // Замена с использованием регулярного выражения
            return Regex.Replace(line, pattern, match => replacements[match.Value]);
        }
       public static string ReplaceRegisters2(string line)
        {

            string pattern = @"\b(esp|ebp)\b(?!\s*\()";


            var replacements = new Dictionary<string, string>
        {

            { "esp", "rsp" },
            { "ebp", "rbp" }
        };


            return Regex.Replace(line, pattern, match => replacements[match.Value]);
        }
    }
}
