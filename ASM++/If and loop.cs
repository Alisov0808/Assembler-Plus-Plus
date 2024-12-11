using System;
using System.Text.RegularExpressions;

namespace Lumin
{
    public unsafe class If_and_loop
    {

        public static void ParsIf(string command, int poz, string file, string inputFilePath, List<string> func, int err, bool whiled, List<string> dllfunc, List<string> macroses, List<string> stringstoaddend, int stringlasted, List<string> peremen, List<string> typeperemen)
        {
            string[] lines5 = File.ReadAllLines(file);
            bool iny2 = false;
            bool wased2 = false;
            string d;
            if (command.TrimStart().StartsWith("if ") || command.TrimStart().StartsWith("else") || command.TrimStart().StartsWith("else if ") || whiled)
            {


                string patternOr = @"(?<![^""]*""[^""]*|[^']*'[^']*)\bor\b";
                string patternOr2 = @"(?<![^""]*""[^""]*|[^']*'[^']*)\band\b";
                string patternOr3 = @"(?<![^""]*""[^""]*|[^']*'[^']*)\bnot\b";
                string patternEquals = @"(?<![^""]*"")\=\=";

                string result = Regex.Replace(command, patternOr, "|");
                result = Regex.Replace(result, patternOr2, "&");
                result = Regex.Replace(result, patternEquals, "=");
                result = Regex.Replace(result, patternOr3, "~");
                result = result.Replace("!=", "~=").Replace("@~", "~@");
                // result=RemoveAtSymbols(result);
                string[] a = result.TrimStart().Split(new char[] { ' ' });
                List<string> parts = new List<string>();
                string currentPart = "";


                char[] operators = new char[] {  '>', '<', '=', '~', '(', ')' };



                if (!result.StartsWith("else if "))
                {
                    if (!result.StartsWith("else"))
                    {
                        if (result.Contains("=") || result.Contains(">") || result.Contains("<") || result.Contains("~"))
                        {
                            bool insideSingleQuote = false;
                            bool insideDoubleQuote = false;

                            for (int i = 0; i < result.Substring(result.IndexOf(" ")).Length; i++)
                            {
                                char c = result.Substring(result.IndexOf(" "))[i];

                            
                                if (c == '\'')
                                {
                                    insideSingleQuote = !insideSingleQuote;
                                }
                                else if (c == '\"')
                                {
                                    insideDoubleQuote = !insideDoubleQuote;
                                }

                                
                                if (!insideSingleQuote && !insideDoubleQuote)
                                {
                                    if (Array.Exists(operators, element => element == c))
                                    {
                                        if (!string.IsNullOrWhiteSpace(currentPart))
                                        {
                                            parts.Add(currentPart);
                                            currentPart = "";
                                        }

                                        if (c != ' ')
                                        {
                                            parts.Add(c.ToString());
                                        }
                                    }
                                    else
                                    {
                                        currentPart += c;
                                    }
                                }
                                else
                                {
                                    currentPart += c; 
                                }
                            }

                           
                            if (!string.IsNullOrWhiteSpace(currentPart))
                            {
                                parts.Add(currentPart);
                            }
                            int buffer = 0;
                            string[] lin = File.ReadAllLines(file);
                            int ccc = lin.Length - 1;
                     
                            for (int i = 0; i < parts.Count; i++)
                            {
                                if (parts[i].Length >= 1)
                                {
                                    Console.WriteLine(parts[i]);
                                    if (parts[i].TrimEnd().EndsWith("@") && parts[i].TrimStart().StartsWith("\""))
                                    {
                                        parts[i] = helpfunc.parsstring(parts[i].Substring(0, parts[i].Length-1), true, stringstoaddend, stringlasted);
                                    }
                                    if (parts[i].TrimEnd().EndsWith(")")&& parts[i].Contains("("))
                                    {
                                        
                                        parts[i] = parts[i].TrimEnd().TrimStart().Substring(0, parts[i].TrimEnd().TrimStart().Length - 1);
                                        int ig = parts[i].IndexOf("(");
                                       
                                        parts[i] = parts[i].Remove(ig, 1).Insert(ig, " "); ////Console.WriteLine(parts[i]);


                                    }
                                    if (!Sas.Parscallc(file, parts[i].TrimStart(), func, poz, peremen, typeperemen, macroses, dllfunc, true, true))
                                    {

                                        if (char.IsLetter(parts[i].Trim().Replace(" ", null)[0]))
                                        { parts[i] = $"[{parts[i]}]"; }
                                    }
                                    else
                                    {
                                        buffer += 4;
                                        File.AppendAllText(file, $"\nmov dword [ebp-{buffer}],eax");
                                        parts[i] = $"dword [ebp-{buffer}]";
                                    }


                                }


                            }
                            if (buffer != 0)
                            {
                                lin = File.ReadAllLines(file);
                                lin[ccc] = lin[ccc].Insert(lin[ccc].Length, $"\nsub esp,{buffer}");

                                File.WriteAllLines(file, lin);
                            }
                            result = a[0] + " " + string.Join(null, parts);
                        }
                        else { result = a[0] + " " + helpfunc.WrapWordsInBrackets(result.Substring(result.IndexOf(" "))); }
                    }
                    else
                    {

                    }
                }
                else
                {
                    if (result.Contains("=") || result.Contains(">") || result.Contains("<") || result.Contains("~"))
                    {
                        bool insideSingleQuote = false;
                        bool insideDoubleQuote = false; 

                        for (int i = 0; i < result.Substring(result.IndexOf(" ")).Length; i++)
                        {
                            char c = result.Substring(result.IndexOf(" "))[i];

                      
                            if (c == '\'')
                            {
                                insideSingleQuote = !insideSingleQuote; 
                            }
                            else if (c == '\"')
                            {
                                insideDoubleQuote = !insideDoubleQuote; 
                            }

                           
                            if (!insideSingleQuote && !insideDoubleQuote)
                            {
                                if (Array.Exists(operators, element => element == c))
                                {
                                    if (!string.IsNullOrWhiteSpace(currentPart))
                                    {
                                        parts.Add(currentPart);
                                        currentPart = "";
                                    }

                                    if (c != ' ')
                                    {
                                        parts.Add(c.ToString());
                                    }
                                }
                                else
                                {
                                    currentPart += c;
                                }
                            }
                            else
                            {
                                currentPart += c; 
                            }
                        }

                   
                        if (!string.IsNullOrWhiteSpace(currentPart))
                        {
                            parts.Add(currentPart);
                        }
                        int buffer = 0;
                        string[] lin = File.ReadAllLines(file);
                        int ccc = lin.Length - 1;
                        //  Console.WriteLine("Части после разделения:");
                        for (int i = 0; i < parts.Count; i++)
                        {
                            if (parts[i].Length >= 1)
                            {

                                if (parts[i].TrimEnd().EndsWith("@") && parts[i].TrimStart().StartsWith("\""))
                                {
                                    parts[i] = helpfunc.parsstring(parts[i].Substring(0, parts[i].Length - 1), true, stringstoaddend, stringlasted);
                                }
                                if (parts[i].TrimEnd().EndsWith(")") && parts[i].Contains("("))
                                {

                                    parts[i] = parts[i].TrimEnd().TrimStart().Substring(0, parts[i].TrimEnd().TrimStart().Length - 1);
                                    int ig = parts[i].IndexOf("(");
                                    parts[i] = parts[i].Remove(ig, 1).Insert(ig, " "); ////Console.WriteLine(parts[i]);


                                }
                                if (!Sas.Parscallc(file, parts[i].TrimStart(), func, poz, peremen, typeperemen, macroses, dllfunc, true, true))
                                {
                                    
                                    if (char.IsLetter(parts[i].Trim().Replace(" ", null)[0]))
                                    { parts[i] = $"[{parts[i]}]"; }
                                }
                                else
                                {
                                    buffer += 4;
                                    File.AppendAllText(file, $"\nmov dword [ebp-{buffer}],eax");
                                    parts[i] = $"[ebp-{buffer}]";
                                }


                            }


                        }
                        if (buffer != 0)
                        {
                            lin = File.ReadAllLines(file);
                            lin[ccc] = lin[ccc].Insert(lin[ccc].Length, $"\nsub esp,{buffer}");

                            File.WriteAllLines(file, lin);
                        }
                        result = a[0] + " " + string.Join(null, parts);
                    }
                    else { result = a[0] + " " + helpfunc.WrapWordsInBrackets(result.Substring(result.IndexOf(" ")).Substring(3)); }
                }
                if (!whiled)
                {
                    if (!command.TrimStart().StartsWith("else if "))
                    {
                        File.AppendAllText(file, "\n." + result.TrimStart());
                    }
                    else
                    {
                        File.AppendAllText(file, "\n." + result.TrimStart().Substring(5).TrimStart().Insert(0, "elseif "));
                    }
                }
                else { File.AppendAllText(file, "\n." + result.TrimStart()); }
            }
            try
            {
                if (command.TrimStart().StartsWith("compare "))
                {
                    string a = command.TrimStart().Substring(8);
                    string[] param = a.Split(',', 2);
                    if (char.IsLetter(param[0][0]) && char.IsLetter(param[1][0]))
                    { File.AppendAllText(file, "\ncmp [" + param[0] + "],[" + param[1] + "]"); }
                    else if (!char.IsLetter(param[0][0]) && char.IsLetter(param[1][0]))
                    { File.AppendAllText(file, "\ncmp " + param[0] + ",[" + param[1] + "]"); }
                    else if (char.IsLetter(param[0][0]) && !char.IsLetter(param[1][0]))
                    { File.AppendAllText(file, "\ncmp [" + param[0] + "]," + param[1]); }

                }
            }
            catch (Exception)
            {

                Console.WriteLine("Error: Invalid compare command syntax! On line: " + (poz + 1)); err++;
            }
            ///////////// 
            if (command.TrimStart().StartsWith("ifgreaterequ"))
            {

                string[] a = command.TrimStart().Split("ifgreaterequ", 2);
                string[] sp = a[1].TrimStart().Split(' ', 2);
                string res = sp[0] += ",";
                res += sp[1];


                File.AppendAllText(file, $"\njge .thenCL00{poz}" + $"\njmp .endLC00h{poz}\r\n.thenCL00{poz}:\nstdcall " + res + $"\n .endLC00h{poz}:");
            }
            else if (command.TrimStart().StartsWith("ifgreater"))
            {

                string[] a = command.TrimStart().Split("ifgreater", 2);
                string[] sp = a[1].TrimStart().Split(' ', 2);
                string res = sp[0] += ",";
                res += sp[1];


                File.AppendAllText(file, $"\njg .thenCL00{poz}" + $"\njmp .endLC00h{poz}\r\n.thenCL00{poz}:\nstdcall " + res + $"\n .endLC00h{poz}:");
            }

            else if (command.TrimStart().StartsWith("iflessequ"))
            {

                string[] a = command.TrimStart().Split("iflessequ", 2);
                string[] sp = a[1].TrimStart().Split(' ', 2);
                string res = sp[0] += ",";
                res += sp[1];


                File.AppendAllText(file, $"\njle .thenCL00{poz}" + $"\njmp .endLC00h{poz}\r\n.thenCL00{poz}:\nstdcall " + res + $"\n .endLC00h{poz}:");
            }
            else if (command.TrimStart().StartsWith("ifless"))
            {

                string[] a = command.TrimStart().Split("ifless", 2);
                string[] sp = a[1].TrimStart().Split(' ', 2);
                string res = sp[0] += ",";
                res += sp[1];


                File.AppendAllText(file, $"\njl .thenCL00{poz}" + $"\njmp .endLC00h{poz}\r\n.thenCL00{poz}:\nstdcall " + res + $"\n .endLC00h{poz}:"); ;
            }
            else if (command.TrimStart().StartsWith("ifnotequ"))
            {

                string[] a = command.TrimStart().Split("ifnotequ", 2);
                string[] sp = a[1].TrimStart().Split(' ', 2);
                string res = sp[0] += ",";
                res += sp[1];


                File.AppendAllText(file, $"\njne .thenCL00{poz}" + $"\njmp .endLC00h{poz}\r\n.thenCL00{poz}:\nstdcall " + res + $"\n .endLC00h{poz}:");
            }
            else if (command.TrimStart().StartsWith("ifequ"))
            {
                string res = "";
                try
                {


                    string[] a = command.TrimStart().Split("ifequ ", 2);
                    string[] sp = a[1].TrimStart().Split(' ', 2);
                    res = sp[0] += ",";
                    res += sp[1];
                }
                catch (Exception)
                {


                }

                File.AppendAllText(file, $"\nje .thenCL00{poz}" + $"\njmp .endLC00h{poz}\r\n.thenCL00{poz}:\nstdcall " + res + $"\n .endLC00h{poz}:");
            }

            else if (command.TrimStart().StartsWith("ifzero"))
            {

                string[] a = command.TrimStart().Split("ifzero", 2);
                string[] sp = a[1].TrimStart().Split(' ', 2);
                string res = sp[0] += ",";
                res += sp[1];


                File.AppendAllText(file, $"\njz .thenCL00{poz}" + $"\njmp .endLC00h{poz}\r\n.thenCL00{poz}:\nstdcall " + res + $"\n .endLC00h{poz}:");
            }
            else if (command.TrimStart().StartsWith("ifnotzero"))
            {
                string[] a = command.TrimStart().Split("ifnotzero", 2);
                string[] sp = a[1].TrimStart().Split(' ', 2);
                string res = sp[0] += ",";
                res += sp[1];


                File.AppendAllText(file, $"\njnz .thenCL00{poz}" + $"\njmp .endLC00h{poz}\r\n.thenCL00{poz}:\nstdcall " + res + $"\n .endLC00h{poz}:");
            }
        }
        static public void Parswhile(string command, int poz, string file, string inputFilePath, List<string> func, int err)
        {
            if (command.TrimStart().StartsWith("while "))
            {
                bool or = false;
                bool and = false;
                string[] parts = command.TrimStart().Split(' ');
                string[] strings = null;
                if (command.Contains(" or "))
                {
                    or = true;
                    strings = command.TrimStart().Split(" or ");
                    parts[1] = strings[0];
                    command = strings[0];
                    //Console.WriteLine(strings[0]);
                    //Console.WriteLine("while " + strings[1]);
                }
                if (command.Contains(" and "))
                {
                    or = true;
                    strings = command.TrimStart().Split(" and ");
                    parts[1] = strings[0];
                    command = strings[0];
                    //Console.WriteLine(strings[0]);
                    //Console.WriteLine("while " + strings[1]);
                }
                if (parts[1].Trim() == "inf")
                {
                    string[] lines = File.ReadAllLines(file);
                    for (int i = poz - 1; i >= 0; i--)
                    {
                        foreach (var term in func)
                        {
                            if (lines[i].Trim().StartsWith("func ") && lines[i].Trim().Contains(term))
                            {
                                string a = lines[i].Replace("func ", ""); a = a.Replace("{", "");
                                File.AppendAllText(file, "\n" + $"jmp {a}");
                                break;
                            }
                        }
                    }
                }
                else if (command.Contains("=") || command.Contains("!") || command.Contains(">") || command.Contains("<") || command.Contains(">=") || command.Contains("<="))
                {
                    if (command.Contains(">") && !command.Contains(">="))
                    {
                        string pattern = @"\d+";
                        string[] partsw = command.TrimStart().Replace("while ", "").Split(">");
                        if (!char.IsLetter(partsw[0].Trim()[0]))
                        {
                            File.AppendAllText(file, $"\nmov eax,{partsw[0]}");
                            partsw[0] = "eax@";
                        }
                        bool was1 = false;
                        string[] lines = File.ReadAllLines(file);
                        string a = null;
                        for (int i = lines.Count() - 1; i >= 0; i--)
                        {

                            if (lines[i].EndsWith(":") && was1 == false)
                            {

                                a = lines[i].Replace(":", null);

                                was1 = true; break;
                            }

                        }
                        bool containsNumbers = false;
                        try
                        {
                            containsNumbers = Regex.IsMatch(partsw[1], pattern);
                        }
                        catch { }
                        bool containsNumbers9 = false;
                        try
                        {
                            containsNumbers9 = Regex.IsMatch(partsw[0], pattern);
                        }
                        catch { }
                        bool sas = false;
                        for (int ia = 0; ia < func.Count(); ia++)
                        {
                            string com = command.Replace(" ", null);
                            if (partsw[1].Trim().StartsWith(func[ia]))
                            {
                                if (char.IsLetter(partsw[1].Trim()[0]) && char.IsLetter(partsw[0].Trim()[0]))
                                {
                                    File.AppendAllText(file, "\n" + $"call {partsw[1]} \n cmp [{partsw[0].Replace("while ", "")}],eax \njg {a}");
                                    // //Console.WriteLine(partsw[0]);
                                }
                                else
                                {
                                    File.AppendAllText(file, "\n" + $"call {partsw[1]} \n cmp eax,{partsw[0].Replace("while ", "")} \njg {a}");
                                }
                                sas = true;
                                break;
                            }
                            if (partsw[0].Trim().StartsWith(func[ia]))
                            {
                                if (char.IsLetter(partsw[0].Trim()[0]) && char.IsLetter(partsw[1].Trim()[0]))
                                {
                                    File.AppendAllText(file, "\n" + $"call {partsw[0]} \n cmp [{partsw[1].Replace("while ", "")}],eax \njg {a}");
                                    // //Console.WriteLine(partsw[0]);
                                }
                                else
                                {
                                    File.AppendAllText(file, "\n" + $"call {partsw[0]} \n cmp eax,{partsw[1].Replace("while ", "")} \njg {a}");
                                }
                                sas = true;
                                break;
                            }
                        }
                        if (!char.IsLetter(partsw[0].Trim()[0]) && !char.IsLetter(partsw[1].Trim()[0]))
                        {
                            if (sas == false)
                            { File.AppendAllText(file, "\n" + $"cmp {partsw[0].Replace("while ", "")}, {partsw[1]}\njg {a}"); }
                        }
                        else if (char.IsLetter(partsw[0].Trim()[0]) && char.IsLetter(partsw[1].Trim()[0]))
                        {
                            if (sas == false)
                            { File.AppendAllText(file, "\n" + $"cmp [{partsw[0].Replace("while ", "")}], [{partsw[1]}]\njg {a}"); }
                        }
                        else if (!char.IsLetter(partsw[0].Trim()[0]) && char.IsLetter(partsw[1].Trim()[0]))
                        {
                            if (sas == false)
                            { File.AppendAllText(file, "\n" + $"cmp {partsw[0].Replace("while ", "")}, [{partsw[1]}]\njg {a}"); }
                        }
                        else if (char.IsLetter(partsw[0].Trim()[0]) && !char.IsLetter(partsw[1].Trim()[0]))
                        {
                            if (sas == false)
                            { File.AppendAllText(file, "\n" + $"cmp [{partsw[0].Replace("while ", "")}], {partsw[1]}\njg {a}"); }
                        }
                    }
                    else if (command.Contains("<") && !command.Contains("<="))
                    {
                        string pattern = @"\d+";
                        string[] partsw = command.TrimStart().Replace("while ", "").Split("<");
                        if (!char.IsLetter(partsw[0].Trim()[0]))
                        {
                            File.AppendAllText(file, $"\nmov eax,{partsw[0]}");
                            partsw[0] = "eax@";
                        }
                        bool was1 = false;
                        string[] lines = File.ReadAllLines(file);
                        string a = null;
                        for (int i = lines.Count() - 1; i >= 0; i--)
                        {

                            if (lines[i].EndsWith(":") && was1 == false)
                            {

                                a = lines[i].Replace(":", null);

                                was1 = true; break;
                            }

                        }
                        bool containsNumbers = false;
                        try
                        {
                            containsNumbers = Regex.IsMatch(partsw[1], pattern);
                        }
                        catch { }
                        bool containsNumbers9 = false;
                        try
                        {
                            containsNumbers9 = Regex.IsMatch(partsw[0], pattern);
                        }
                        catch { }
                        bool sas = false;
                        for (int ia = 0; ia < func.Count(); ia++)
                        {
                            string com = command.Replace(" ", null);
                            if (partsw[1].Trim().StartsWith(func[ia]))
                            {
                                if (char.IsLetter(partsw[1].Trim()[0]) && char.IsLetter(partsw[0].Trim()[0]))
                                {
                                    File.AppendAllText(file, "\n" + $"call {partsw[1]} \n cmp [{partsw[0].Replace("while ", "")}],eax \njl {a}");
                                    // //Console.WriteLine(partsw[0]);
                                }
                                else
                                {
                                    File.AppendAllText(file, "\n" + $"call {partsw[1]} \n cmp eax,{partsw[0].Replace("while ", "")} \njl {a}");
                                }
                                sas = true;
                                break;
                            }
                            if (partsw[0].Trim().StartsWith(func[ia]))
                            {
                                if (char.IsLetter(partsw[0].Trim()[0]) && char.IsLetter(partsw[1].Trim()[0]))
                                {
                                    File.AppendAllText(file, "\n" + $"call {partsw[0]} \n cmp [{partsw[1].Replace("while ", "")}],eax \njl {a}");
                                    // //Console.WriteLine(partsw[0]);
                                }
                                else
                                {
                                    File.AppendAllText(file, "\n" + $"call {partsw[0]} \n cmp {partsw[1].Replace("while ", "")},eax \njl {a}");
                                }
                                sas = true;
                                break;
                            }
                        }
                        if (!char.IsLetter(partsw[0].Trim()[0]) && !char.IsLetter(partsw[1].Trim()[0]))
                        {
                            if (sas == false)
                            { File.AppendAllText(file, "\n" + $"cmp {partsw[0].Replace("while ", "")}, {partsw[1]}\njl {a}"); }
                        }
                        else if (char.IsLetter(partsw[0].Trim()[0]) && char.IsLetter(partsw[1].Trim()[0]))
                        {
                            if (sas == false)
                            { File.AppendAllText(file, "\n" + $"cmp [{partsw[0].Replace("while ", "")}], [{partsw[1]}]\njl {a}"); }
                        }
                        else if (!char.IsLetter(partsw[0].Trim()[0]) && char.IsLetter(partsw[1].Trim()[0]))
                        {
                            if (sas == false)
                            { File.AppendAllText(file, "\n" + $"cmp {partsw[0].Replace("while ", "")}, [{partsw[1]}]\njl {a}"); }
                        }
                        else if (char.IsLetter(partsw[0].Trim()[0]) && !char.IsLetter(partsw[1].Trim()[0]))
                        {
                            if (sas == false)
                            { File.AppendAllText(file, "\n" + $"cmp [{partsw[0].Replace("while ", "")}], {partsw[1]}\njl {a}"); }
                        }
                    }
                    else if (command.Contains("<="))
                    {
                        string pattern = @"\d+";
                        string[] partsw = command.TrimStart().Replace("while ", "").Split("<=", 2);
                        if (!char.IsLetter(partsw[0].Trim()[0]))
                        {
                            File.AppendAllText(file, $"\nmov eax,{partsw[0]}");
                            partsw[0] = "eax@";
                        }
                        bool was1 = false;
                        string[] lines = File.ReadAllLines(file);
                        string a = null;
                        for (int i = lines.Count() - 1; i >= 0; i--)
                        {

                            if (lines[i].EndsWith(":") && was1 == false)
                            {

                                a = lines[i].Replace(":", null);

                                was1 = true; break;
                            }

                        }
                        bool containsNumbers = false;
                        try
                        {
                            containsNumbers = Regex.IsMatch(partsw[1], pattern);
                        }
                        catch { }
                        bool containsNumbers9 = false;
                        try
                        {
                            containsNumbers9 = Regex.IsMatch(partsw[0], pattern);
                        }
                        catch { }
                        bool sas = false;
                        for (int ia = 0; ia < func.Count(); ia++)
                        {
                            string com = command.Replace(" ", null);
                            if (partsw[1].Trim().StartsWith(func[ia]))
                            {
                                if (char.IsLetter(partsw[1].Trim()[0]) && char.IsLetter(partsw[0].Trim()[0]))
                                {
                                    File.AppendAllText(file, "\n" + $"call {partsw[1]} \n cmp [{partsw[0].Replace("while ", "")}],eax \njl {a} \n cmp [{partsw[0].Replace("while ", "")}],eax \nje {a}");
                                    //Console.WriteLine(partsw[0]);
                                }
                                else
                                {
                                    File.AppendAllText(file, "\n" + $"call {partsw[1]} \n cmp eax,{partsw[0].Replace("while ", "")} \njl {a}\n cmp eax,{partsw[0].Replace("while ", "")} \nje {a}");
                                }
                                sas = true;
                                break;
                            }
                            if (partsw[0].Trim().StartsWith(func[ia]))
                            {
                                if (char.IsLetter(partsw[0].Trim()[0]) && char.IsLetter(partsw[1].Trim()[0]))
                                {
                                    File.AppendAllText(file, "\n" + $"call {partsw[0]} \n cmp [{partsw[1].Replace("while ", "")}],eax \njl {a} \n cmp [{partsw[1].Replace("while ", "")}],eax \nje {a}");
                                    //Console.WriteLine(partsw[0]);
                                }
                                else
                                {
                                    File.AppendAllText(file, "\n" + $"call {partsw[0]} \n cmp eax,{partsw[1].Replace("while ", "")} \njl {a} \ncmp eax,{partsw[1].Replace("while ", "")} \nje {a}");
                                }
                                sas = true;
                                break;
                            }
                        }
                        if (!char.IsLetter(partsw[0].Trim()[0]) && !char.IsLetter(partsw[1].Trim()[0]))
                        {
                            if (sas == false)
                            { File.AppendAllText(file, "\n" + $"cmp {partsw[0].Replace("while ", "")}, {partsw[1]}\njl {a}\ncmp {partsw[0].Replace("while ", "")}, {partsw[1]}\nje {a}"); }
                        }
                        else if (char.IsLetter(partsw[0].Trim()[0]) && char.IsLetter(partsw[1].Trim()[0]))
                        {
                            if (sas == false)
                            { File.AppendAllText(file, "\n" + $"cmp [{partsw[0].Replace("while ", "")}], [{partsw[1]}]\njl {a}\ncmp [{partsw[0].Replace("while ", "")}], [{partsw[1]}]\nje {a}"); }
                        }
                        else if (!char.IsLetter(partsw[0].Trim()[0]) && char.IsLetter(partsw[1].Trim()[0]))
                        {
                            if (sas == false)
                            { File.AppendAllText(file, "\n" + $"cmp {partsw[0].Replace("while ", "")}, [{partsw[1]}]\njl {a}\ncmp {partsw[0].Replace("while ", "")},[ {partsw[1]}]\nje {a}"); }
                        }
                        else if (char.IsLetter(partsw[0].Trim()[0]) && !char.IsLetter(partsw[1].Trim()[0]))
                        {
                            if (sas == false)
                            { File.AppendAllText(file, "\n" + $"cmp [{partsw[0].Replace("while ", "")}], {partsw[1]}\njl {a}\ncmp [{partsw[0].Replace("while ", "")}], {partsw[1]}\nje {a}"); }
                        }
                    }
                    else if (command.Contains(">="))
                    {
                        string pattern = @"\d+";
                        string[] partsw = command.TrimStart().Replace("while ", "").Split(">=", 2);
                        if (!char.IsLetter(partsw[0].Trim()[0]))
                        {
                            File.AppendAllText(file, $"\nmov eax,{partsw[0]}");
                            partsw[0] = "eax@";
                        }
                        bool was1 = false;
                        string[] lines = File.ReadAllLines(file);
                        string a = null;
                        for (int i = lines.Count() - 1; i >= 0; i--)
                        {

                            if (lines[i].EndsWith(":") && was1 == false)
                            {

                                a = lines[i].Replace(":", null);

                                was1 = true; break;
                            }

                        }
                        bool containsNumbers = false;
                        try
                        {
                            containsNumbers = Regex.IsMatch(partsw[1], pattern);
                        }
                        catch { }
                        bool containsNumbers9 = false;
                        try
                        {
                            containsNumbers9 = Regex.IsMatch(partsw[0], pattern);
                        }
                        catch { }
                        bool sas = false;
                        for (int ia = 0; ia < func.Count(); ia++)
                        {
                            string com = command.Replace(" ", null);
                            if (partsw[1].Trim().StartsWith(func[ia]))
                            {
                                if (char.IsLetter(partsw[1].Trim()[0]) && char.IsLetter(partsw[0].Trim()[0]))
                                {
                                    File.AppendAllText(file, "\n" + $"call {partsw[1]} \n cmp [{partsw[0].Replace("while ", "")}],eax \njg {a} \n cmp [{partsw[0].Replace("while ", "")}],eax \nje {a}");
                                    //Console.WriteLine(partsw[0]);
                                }
                                else
                                {
                                    File.AppendAllText(file, "\n" + $"call {partsw[1]} \n cmp eax,{partsw[0].Replace("while ", "")} \njg {a}\n cmp eax,{partsw[1].Replace("while ", "")} \nje {a}");
                                }
                                sas = true;
                                break;
                            }
                            if (partsw[0].Trim().StartsWith(func[ia]))
                            {
                                if (char.IsLetter(partsw[0].Trim()[0]) && char.IsLetter(partsw[1].Trim()[0]))
                                {
                                    File.AppendAllText(file, "\n" + $"call {partsw[0]} \n cmp [{partsw[1].Replace("while ", "")}],eax \njg {a} \n cmp [{partsw[1].Replace("while ", "")}],eax \nje {a}");
                                    //Console.WriteLine(partsw[0]);
                                }
                                else
                                {
                                    File.AppendAllText(file, "\n" + $"call {partsw[0]} \ncmp eax,{partsw[1].Replace("while ", "")} \njg {a} \n cmp eax,{partsw[1].Replace("while ", "")} \nje {a}");
                                }
                                sas = true;
                                break;
                            }
                        }
                        if (!char.IsLetter(partsw[0].Trim()[0]) && !char.IsLetter(partsw[1].Trim()[0]))
                        {
                            if (sas == false)
                            { File.AppendAllText(file, "\n" + $"cmp {partsw[0].Replace("while ", "")}, {partsw[1]}\njg {a}\ncmp {partsw[0].Replace("while ", "")}, {partsw[1]}\nje {a}"); }
                        }
                        else if (char.IsLetter(partsw[0].Trim()[0]) && char.IsLetter(partsw[1].Trim()[0]))
                        {
                            if (sas == false)
                            { File.AppendAllText(file, "\n" + $"cmp [{partsw[0].Replace("while ", "")}], [{partsw[1]}]\njg {a}\ncmp [{partsw[0].Replace("while ", "")}], [{partsw[1]}]\nje {a}"); }
                        }
                        else if (!char.IsLetter(partsw[0].Trim()[0]) && char.IsLetter(partsw[1].Trim()[0]))
                        {
                            if (sas == false)
                            { File.AppendAllText(file, "\n" + $"cmp {partsw[0].Replace("while ", "")}, [{partsw[1]}]\njg {a}\ncmp {partsw[0].Replace("while ", "")},[ {partsw[1]}]\nje {a}"); }
                        }
                        else if (char.IsLetter(partsw[0].Trim()[0]) && !char.IsLetter(partsw[1].Trim()[0]))
                        {
                            if (sas == false)
                            { File.AppendAllText(file, "\n" + $"cmp [{partsw[0].Replace("while ", "")}], {partsw[1]}\njg {a}\ncmp [{partsw[0].Replace("while ", "")}], {partsw[1]}\nje {a}"); }
                        }
                    }
                    else if (command.Contains("==") && !command.Contains("!"))
                    {
                        string pattern = @"\d+";
                        string[] partsw = command.TrimStart().Replace("while ", "").Split("==", 2);
                        if (!char.IsLetter(partsw[0].Trim()[0]))
                        {
                            File.AppendAllText(file, $"\nmov eax,{partsw[0]}");
                            partsw[0] = "eax@";
                        }
                        bool was1 = false;
                        string[] lines = File.ReadAllLines(file);
                        string a = null;
                        for (int i = lines.Count() - 1; i >= 0; i--)
                        {

                            if (lines[i].EndsWith(":") && was1 == false)
                            {

                                a = lines[i].Replace(":", null);

                                was1 = true; break;

                            }

                        }
                        bool containsNumbers = false;
                        try
                        {
                            containsNumbers = Regex.IsMatch(partsw[1], pattern);
                        }
                        catch { }
                        bool containsNumbers9 = false;
                        try
                        {
                            containsNumbers9 = Regex.IsMatch(partsw[0], pattern);
                        }
                        catch { }
                        bool sas = false;
                        for (int ia = 0; ia < func.Count(); ia++)
                        {
                            string com = command.Replace(" ", null);
                            if (partsw[1].Trim().StartsWith(func[ia]))
                            {
                                if (char.IsLetter(partsw[1].Trim()[0]) && char.IsLetter(partsw[0].Trim()[0]))
                                {
                                    File.AppendAllText(file, "\n" + $"call {partsw[1]} \n cmp [{partsw[0].Replace("while ", "")}],eax \nje {a}");
                                    //Console.WriteLine(partsw[0]);
                                }
                                else
                                {
                                    File.AppendAllText(file, "\n" + $"call {partsw[1]} \ncmp eax,{partsw[0].Replace("while ", "")} \nje {a}");
                                }
                                sas = true;
                                break;
                            }
                            if (partsw[0].Trim().StartsWith(func[ia]))
                            {
                                if (char.IsLetter(partsw[0].Trim()[0]) && char.IsLetter(partsw[1].Trim()[0]))
                                {
                                    File.AppendAllText(file, "\n" + $"call {partsw[0]} \n cmp [{partsw[1].Replace("while ", "")}],eax \nje {a}");
                                    //Console.WriteLine(partsw[0]);
                                }
                                else
                                {
                                    File.AppendAllText(file, "\n" + $"call {partsw[0]} \ncmp eax,{partsw[1].Replace("while ", "")}\nje {a}");
                                }
                                sas = true;
                                break;
                            }
                        }
                        if (!char.IsLetter(partsw[0].Trim()[0]) && !char.IsLetter(partsw[1].Trim()[0]))
                        {
                            if (sas == false)
                            { File.AppendAllText(file, "\n" + $"cmp {partsw[0].Replace("while ", "")}, {partsw[1]}\nje {a}"); }
                        }
                        else if (char.IsLetter(partsw[0].Trim()[0]) && char.IsLetter(partsw[1].Trim()[0]))
                        {
                            if (sas == false)
                            { File.AppendAllText(file, "\n" + $"cmp [{partsw[0].Replace("while ", "")}], [{partsw[1]}]\nje {a}"); }
                        }
                        else if (!char.IsLetter(partsw[0].Trim()[0]) && char.IsLetter(partsw[1].Trim()[0]))
                        {
                            if (sas == false)
                            { File.AppendAllText(file, "\n" + $"cmp {partsw[0].Replace("while ", "")}, [{partsw[1]}]\nje {a}"); }
                        }
                        else if (char.IsLetter(partsw[0].Trim()[0]) && !char.IsLetter(partsw[1].Trim()[0]))
                        {
                            if (sas == false)
                            { File.AppendAllText(file, "\n" + $"cmp [{partsw[0].Replace("while ", "")}], {partsw[1]}\nje {a}"); }
                        }
                    }
                    else if (command.Contains("!"))
                    {
                        string pattern = @"\d+";
                        string[] partsw = command.TrimStart().Replace("while ", "").Split("!=");
                        if (!char.IsLetter(partsw[0].Trim()[0]))
                        {
                            File.AppendAllText(file, $"\nmov eax,{partsw[0]}");
                            partsw[0] = "eax@";
                        }
                        bool was1 = false;
                        string[] lines = File.ReadAllLines(file);
                        string a = null;
                        for (int i = lines.Count() - 1; i >= 0; i--)
                        {

                            if (lines[i].EndsWith(":") && was1 == false)
                            {

                                a = lines[i].Replace(":", null);

                                was1 = true; break;
                            }

                        }
                        bool containsNumbers = false;
                        try
                        {
                            containsNumbers = Regex.IsMatch(partsw[1], pattern);
                        }
                        catch { }
                        bool containsNumbers9 = false;
                        try
                        {
                            containsNumbers9 = Regex.IsMatch(partsw[0], pattern);
                        }
                        catch { }
                        bool sas = false;
                        for (int ia = 0; ia < func.Count(); ia++)
                        {
                            string com = command.Replace(" ", null);
                            if (partsw[1].Trim().StartsWith(func[ia]))
                            {
                                if (char.IsLetter(partsw[1].Trim()[0]) && char.IsLetter(partsw[0].Trim()[0]))
                                {
                                    File.AppendAllText(file, "\n" + $"call {partsw[1]} \n cmp [{partsw[0].Replace("while ", "")}],eax \njne {a}");
                                    //Console.WriteLine(partsw[0]);
                                }
                                else
                                {
                                    File.AppendAllText(file, "\n" + $"call {partsw[1]} \ncmp eax,{partsw[0].Replace("while ", "")} \njne {a}");
                                }
                                sas = true;
                                break;
                            }
                            if (partsw[0].Trim().StartsWith(func[ia]))
                            {
                                if (char.IsLetter(partsw[0].Trim()[0]) && char.IsLetter(partsw[1].Trim()[0]))
                                {
                                    File.AppendAllText(file, "\n" + $"call {partsw[0]} \n cmp [{partsw[1].Replace("while ", "")}],eax \njne {a}");
                                    //Console.WriteLine(partsw[0]);
                                }
                                else
                                {
                                    File.AppendAllText(file, "\n" + $"call {partsw[0]} \n cmp eax,{partsw[1].Replace("while ", "")}\njne {a}");
                                }
                                sas = true;
                                break;
                            }
                        }
                        if (!char.IsLetter(partsw[0].Trim()[0]) && !char.IsLetter(partsw[1].Trim()[0]))
                        {
                            if (sas == false)
                            { File.AppendAllText(file, "\n" + $"cmp {partsw[0].Replace("while ", "")}, {partsw[1]}\njne {a}"); }
                        }
                        else if (char.IsLetter(partsw[0].Trim()[0]) && char.IsLetter(partsw[1].Trim()[0]))
                        {
                            if (sas == false)
                            { File.AppendAllText(file, "\n" + $"cmp [{partsw[0].Replace("while ", "")}], [{partsw[1]}]\njne {a}"); }
                        }
                        else if (!char.IsLetter(partsw[0].Trim()[0]) && char.IsLetter(partsw[1].Trim()[0]))
                        {
                            if (sas == false)
                            { File.AppendAllText(file, "\n" + $"cmp {partsw[0].Replace("while ", "")}, [{partsw[1]}]\njne {a}"); }
                        }
                        else if (char.IsLetter(partsw[0].Trim()[0]) && !char.IsLetter(partsw[1].Trim()[0]))
                        {
                            if (sas == false)
                            { File.AppendAllText(file, "\n" + $"cmp [{partsw[0].Replace("while ", "")}], {partsw[1]}\njne {a}"); }
                        }
                    }
                    if (or) Parswhile("while " + strings[1], poz, file, inputFilePath, func, err);
                    if (and) Parswhile("while " + strings[1], poz, file, inputFilePath, func, err);
                }
                else
                {

                    bool was1 = false;
                    string[] lines = File.ReadAllLines(file);
                    string[] ded = parts[1].Split(",", 2);
                    ded[1] = ded[1].Replace(" ", null);
                    ded[0] = ded[0].Replace(" ", null);
                    string[] ded2 = ded[1].Split(",", 2);
                    ded2[0] = ded2[0].Replace(" ", null);
                    ded2[1] = ded2[1].Replace(" ", null);
                    //   //Console.WriteLine(parts[0] + " " + ded[1] + " " + ded2[1]);
                    string[] string4 = File.ReadAllLines(file);
                    List<string> lines5 = string4.Where(item => !string.IsNullOrWhiteSpace(item)).ToList();
                    string funec = null;
                    bool inr = false;
                    for (int i2 = lines5.Count() - 1; i2 > 0; i2--)
                    {

                        if (lines5[i2].TrimEnd().EndsWith(":"))
                        {
                            funec = lines5[i2].TrimStart().Substring(0, lines5[i2].TrimStart().Length - 1);
                            inr = true;
                            break;
                        }
                        if (lines5[i2].TrimStart().StartsWith("proc "))
                        {
                            funec = lines5[i2].TrimStart().Substring(lines5[i2].IndexOf(' ')).Substring(5);
                            // //Console.WriteLine(funec + " f");
                            inr = true;

                            break;
                        }
                    }
                    for (int i = poz - 1; i >= 0; i--)
                    {
                        //foreach (var term in func)
                        //{ //Console.WriteLine("d");
                        //    if (lines[i].TrimStart().StartsWith("func ") && lines[i].Replace(" ", null).Replace("func", null) == term && was1 == false || lines[i].TrimStart().StartsWith("label ") && lines[i].Replace(" ", null).Replace("label", null).Replace(":", null) == term && was1 == false)
                        //    {

                        //string a = term;
                        //a = a.Replace("{", "");

                        if (ded2[1].Replace(" ", null) == "greater")
                        {

                            if (Char.IsLetter(ded[1][0]))
                            {
                                if (inr)
                                {
                                    File.AppendAllText(file, "\n" + $"\ndec [{ded[0]}]\n cmp [{ded[0]}], [{ded2[0]}]\njge {funec}"); break;
                                }
                                else { File.AppendAllText(file, "\n" + $"\ndec [{ded[0]}]\n cmp [{ded[0]}], [{ded2[0]}]\njge {funec}"); break; }
                            }
                            else
                            {
                                if (inr)
                                {
                                    File.AppendAllText(file, "\n" + $"\ndec [{ded[0]}]\n cmp [{ded[0]}], {ded2[0]}\njge {funec}"); break;
                                }
                                else { File.AppendAllText(file, "\n" + $"\ndec [{ded[0]}]\n cmp [{ded[0]}], {ded2[0]}\njge {funec}"); break; }
                            }
                        }
                        if (ded2[1].Replace(" ", null) == "less")
                        {
                            if (Char.IsLetter(ded[1][0]))
                            {
                                if (inr)
                                {
                                    File.AppendAllText(file, "\n" + $"\ninc [{ded[0]}]\n cmp [{ded[0]}], [{ded2[0]}]\njle {funec}"); break;
                                }
                                else { File.AppendAllText(file, "\n" + $"\ninc [{ded[0]}]\n cmp [{ded[0]}], [{ded2[0]}]\njle {funec}"); break; }
                            }
                            else
                            {
                                if (inr)
                                {
                                    File.AppendAllText(file, "\n" + $"\ninc [{ded[0]}]\n cmp [{ded[0]}], {ded2[0]}\njle {funec}"); break;
                                }
                                else { File.AppendAllText(file, "\n" + $"\ninc [{ded[0]}]\n cmp [{ded[0]}], {ded2[0]}\njle {funec}"); break; }
                            }

                        }
                        if (ded2[1].Replace(" ", null) == "greater,reg")
                        {

                            if (Char.IsLetter(ded[1][0]))
                            {
                                if (inr)
                                {
                                    File.AppendAllText(file, "\n" + $"\ndec {ded[0]}\n cmp {ded[0]}, [{ded2[0]}]\njge {funec}"); break;
                                }
                                else { File.AppendAllText(file, "\n" + $"\ndec {ded[0]}\n cmp {ded[0]}, [{ded2[0]}]\njge {funec}"); break; }
                            }
                            else
                            {
                                if (inr)
                                {
                                    File.AppendAllText(file, "\n" + $"\ndec {ded[0]}\n cmp {ded[0]}, {ded2[0]}\njge {funec}"); break;
                                }
                                else { File.AppendAllText(file, "\n" + $"\ndec {ded[0]}\n cmp {ded[0]}, {ded2[0]}\njge {funec}"); break; }
                            }
                        }
                        if (ded2[1].Replace(" ", null) == "less,reg")
                        {
                            if (Char.IsLetter(ded[1][0]))
                            {
                                if (inr)
                                {
                                    File.AppendAllText(file, "\n" + $"\ninc {ded[0]}\n cmp {ded[0]}, [{ded2[0]}]\njle {funec}"); break;
                                }
                                else { File.AppendAllText(file, "\n" + $"\ninc {ded[0]}\n cmp {ded[0]}, [{ded2[0]}]\njle {funec}"); break; }
                            }
                            else
                            {
                                if (inr)
                                {
                                    File.AppendAllText(file, "\n" + $"\ninc {ded[0]}\n cmp {ded[0]}, {ded2[0]}\njle {funec}"); break;
                                }
                                else { File.AppendAllText(file, "\n" + $"\ninc {ded[0]}\n cmp {ded[0]}, {ded2[0]}\njle {funec}"); break; }
                            }

                        }
                        was1 = true; break;
                        //        }
                        //    }
                    }
                }

            }


        }
        static public string Parsfor(string command, int poz, string file, string inputFilePath, List<string> func, int err, List<string> peremen, List<string> typeperemen, bool check = false)
        {
            string[] a = command.Trim().Substring(3).Split(',');
            string[] string4 = File.ReadAllLines(file);
            List<string> lines5 = string4.Where(item => !string.IsNullOrWhiteSpace(item)).ToList();
            string funec = null;
            bool inr = false;
            for (int i2 = lines5.Count() - 1; i2 > 0; i2--)
            {

                if (lines5[i2].TrimEnd().EndsWith(":"))
                {
                    funec = lines5[i2].TrimStart().Substring(0, lines5[i2].TrimStart().Length - 1);
                    inr = true;
                    break;
                }
                if (lines5[i2].TrimStart().StartsWith("proc "))
                {
                    funec = lines5[i2].TrimStart().Substring(lines5[i2].IndexOf(' ')).Substring(5);
                    inr = true;

                    break;
                }
            }
            if (!check)
            {

                if (!char.IsLetter(a[2][0]))
                {
                    File.AppendAllText(file, $"\ncmp [{a[0]}],{a[1]}\njne {funec}");
                    return $"\ncmp [{a[0]}],{a[1]}\njne {funec}";
                }
                else
                {
                    int isp = Parser.isperemen(a[2], peremen, typeperemen);
                    if (typeperemen[isp] == "dword")
                    {
                        File.AppendAllText(file, $"\nmov ebx,[{a[2]}]\ncmp [{a[0]}],ebx\njne {funec}");
                        return $"\nmov ebx,[{a[2]}]\ncmp [{a[0]}],ebx\njne {funec}";
                    }
                    else if (typeperemen[isp] == "byte")
                    {
                        File.AppendAllText(file, $"\nmov bl,[{a[2]}\ncmp [{a[0]}],bl\njne {funec}");
                        return $"\nmov bl,[{a[2]}]\ncmp [{a[0]}],bl\njne {funec}";
                    }
                    else if (typeperemen[isp] == "word" || typeperemen[isp] == "ubyte")
                    {
                        File.AppendAllText(file, $"\nmov ax,[{a[2]}]\ncmp [{a[0]}],{a[1]}\njne {funec}");
                        return $"\nmov ax,[{a[2]}]\ncmp [{a[0]}],ax\njne {funec}";
                    }
                    else if (typeperemen[isp] == "tword" || typeperemen[isp] == "qword")
                    {
                        File.AppendAllText(file, $"\nmov rax,[{a[2]}]\ncmp [{a[0]}],{a[1]}\njne {funec}");
                        return $"\nmov rax,[{a[2]}]\ncmp [{a[0]}],rax\njne {funec}";
                    }
                }
            }
            else
            {
                if (a[1].StartsWith("-"))
                {
                    if (!char.IsLetter(a[2][0]))
                    {
                        return $"\nsub [{a[0]}],{a[2].Replace("-", null)}\ncmp [{a[0]}],{a[1]}\njne {funec}";
                    }
                    else
                    {
                        int isp = Parser.isperemen(a[2], peremen, typeperemen);
                        if (typeperemen[isp] == "dword")
                        {
                            // File.AppendAllText(file, $"\nmov ebx,[{a[2]}]\nadd [{a[0]}],ebx\ncmp [{a[0]}],ebx\njne {funec}");
                            return $"\nmov ebx,[{a[2]}]\nadd [{a[0]}],ebx\ncmp [{a[0]}],ebx\njne {funec}";
                        }
                        else if (typeperemen[isp] == "byte")
                        {
                            // File.AppendAllText(file, $"\nmov bl,[{a[2]}]\nadd [{a[0]}],bl\ncmp [{a[0]}],bl\njne {funec}");
                            return $"\nmov bl,[{a[2]}]\nadd [{a[0]}],bl\ncmp [{a[0]}],bl\njne {funec}";
                        }
                        else if (typeperemen[isp] == "word" || typeperemen[isp] == "ubyte")
                        {
                            // File.AppendAllText(file, $"\nmov ax,[{a[2]}]\nadd [{a[0]}],ax\ncmp [{a[0]}],{a[1]}\njne {funec}");
                            return $"\nmov ax,[{a[2]}]\nadd [{a[0]}],ax\ncmp [{a[0]}],ax\njne {funec}";
                        }
                        else if (typeperemen[isp] == "tword" || typeperemen[isp] == "qword")
                        {
                            // File.AppendAllText(file, $"\nmov rax,[{a[2]}]\nadd [{a[0]}],rax\ncmp [{a[0]}],{a[1]}\njne {funec}");
                            return $"\nmov rax,[{a[2]}]\nadd [{a[0]}],rax\ncmp [{a[0]}],rax\njne {funec}";
                        }
                    }
                }
                else
                {
                    if (!char.IsLetter(a[2][0]))
                    {
                        return $"\nadd [{a[0]}],{a[2]}\ncmp [{a[0]}],{a[1]}\njne {funec}";
                    }
                    else
                    {
                        int isp = Parser.isperemen(a[2], peremen, typeperemen);
                        if (typeperemen[isp] == "dword")
                        {
                            // File.AppendAllText(file, $"\nmov ebx,[{a[2]}]\nadd [{a[0]}],ebx\ncmp [{a[0]}],ebx\njne {funec}");
                            return $"\nmov ebx,[{a[2]}]\nadd [{a[0]}],ebx\ncmp [{a[0]}],ebx\njne {funec}";
                        }
                        else if (typeperemen[isp] == "byte")
                        {
                            // File.AppendAllText(file, $"\nmov bl,[{a[2]}]\nadd [{a[0]}],bl\ncmp [{a[0]}],bl\njne {funec}");
                            return $"\nmov bl,[{a[2]}]\nadd [{a[0]}],bl\ncmp [{a[0]}],bl\njne {funec}";
                        }
                        else if (typeperemen[isp] == "word" || typeperemen[isp] == "ubyte")
                        {

                            // File.AppendAllText(file, $"\nmov ax,[{a[2]}]\nadd [{a[0]}],ax\ncmp [{a[0]}],{a[1]}\njne {funec}");
                            return $"\nmov ax,[{a[2]}]\nadd [{a[0]}],ax\ncmp [{a[0]}],ax\njne {funec}";
                        }
                        else if (typeperemen[isp] == "tword" || typeperemen[isp] == "qword")
                        {
                            // File.AppendAllText(file, $"\nmov rax,[{a[2]}]\nadd [{a[0]}],rax\ncmp [{a[0]}],{a[1]}\njne {funec}");
                            return $"\nmov rax,[{a[2]}]\nadd [{a[0]}],rax\ncmp [{a[0]}],rax\njne {funec}";
                        }
                    }
                }
            }
            return default;
        }
        static public void ParsLoop(string command, int poz, string file, string inputFilePath, List<string> func, int err)
        {
            bool f = false;
            string d = null;
            try
            {

                if (command.TrimStart().StartsWith("loop "))
                {
                    string[] parts = command.TrimStart().Split(' ', 2);
                    if (parts[1].Trim() == "inf")
                    {
                        string[] string4 = File.ReadAllLines(file);
                        List<string> lines5 = string4.Where(item => !string.IsNullOrWhiteSpace(item)).ToList();
                        string funec = null;
                        bool inr = false;
                        for (int i2 = lines5.Count() - 1; i2 > 0; i2--)
                        {

                            if (lines5[i2].Trim().EndsWith(":"))
                            {
                                funec = lines5[i2].Trim().Substring(0, lines5[i2].Length - 1);
                                break;
                            }
                            if (lines5[i2].TrimStart().StartsWith("proc "))
                            {
                                funec = lines5[i2].TrimStart().Substring(lines5[i2].IndexOf(' ')).Substring(5);
                                // //Console.WriteLine(funec +" f");
                                inr = true;
                                break;
                            }
                        }
                        if (inr)
                        {
                            File.AppendAllText(file, "\n" + $"jmp {funec}");
                        }
                        else
                        {
                            File.AppendAllText(file, "\n" + $"jmp {funec}");
                        }
                        string[] lines = File.ReadAllLines(file);






                    }
                    else if (command.Contains("=") || command.Contains("!") || command.Contains(">") || command.Contains("<") || command.Contains(">=") || command.Contains("<="))
                    {
                        Parswhile("while" + command.TrimStart().Substring(4), poz, file, inputFilePath, func, err);

                    }

                    else
                    {
                        bool was1 = false;
                        string[] lines = File.ReadAllLines(file);
                        string[] ded = parts[1].Split(",", 2);
                        ded[1] = ded[1].Replace(" ", null);
                        ded[0] = ded[0].Replace(" ", null);
                        string[] ded2 = ded[1].Split(",", 2);
                        ded2[0] = ded2[0].Replace(" ", null);
                        ded2[1] = ded2[1].Replace(" ", null);
                        //   //Console.WriteLine(parts[0] + " " + ded[1] + " " + ded2[1]);
                        string[] string4 = File.ReadAllLines(file);
                        List<string> lines5 = string4.Where(item => !string.IsNullOrWhiteSpace(item)).ToList();
                        string funec = null;
                        bool inr = false;
                        for (int i2 = lines5.Count() - 1; i2 > 0; i2--)
                        {

                            if (lines5[i2].TrimEnd().EndsWith(":"))
                            {
                                funec = lines5[i2].TrimStart().Substring(0, lines5[i2].TrimStart().Length - 1);
                                inr = true;
                                break;
                            }
                            if (lines5[i2].TrimStart().StartsWith("proc "))
                            {
                                funec = lines5[i2].TrimStart().Substring(lines5[i2].IndexOf(' ')).Substring(5);
                                // //Console.WriteLine(funec + " f");
                                inr = true;

                                break;
                            }
                        }
                        for (int i = poz - 1; i >= 0; i--)
                        {
                            //foreach (var term in func)
                            //{ //Console.WriteLine("d");
                            //    if (lines[i].TrimStart().StartsWith("func ") && lines[i].Replace(" ", null).Replace("func", null) == term && was1 == false || lines[i].TrimStart().StartsWith("label ") && lines[i].Replace(" ", null).Replace("label", null).Replace(":", null) == term && was1 == false)
                            //    {

                            //string a = term;
                            //a = a.Replace("{", "");

                            if (ded2[1].Replace(" ", null) == "greater")
                            {

                                if (Char.IsLetter(ded[1][0]))
                                {
                                    if (inr)
                                    {
                                        File.AppendAllText(file, "\n" + $"\ndec [{ded[0]}]\n cmp [{ded[0]}], [{ded2[0]}]\njge {funec}"); break;
                                    }
                                    else { File.AppendAllText(file, "\n" + $"\ndec [{ded[0]}]\n cmp [{ded[0]}], [{ded2[0]}]\njge {funec}"); break; }
                                }
                                else
                                {
                                    if (inr)
                                    {
                                        File.AppendAllText(file, "\n" + $"\ndec [{ded[0]}]\n cmp [{ded[0]}], {ded2[0]}\njge {funec}"); break;
                                    }
                                    else { File.AppendAllText(file, "\n" + $"\ndec [{ded[0]}]\n cmp [{ded[0]}], {ded2[0]}\njge {funec}"); break; }
                                }
                            }
                            if (ded2[1].Replace(" ", null) == "less")
                            {
                                if (Char.IsLetter(ded[1][0]))
                                {
                                    if (inr)
                                    {
                                        File.AppendAllText(file, "\n" + $"\ninc [{ded[0]}]\n cmp [{ded[0]}], [{ded2[0]}]\njle {funec}"); break;
                                    }
                                    else { File.AppendAllText(file, "\n" + $"\ninc [{ded[0]}]\n cmp [{ded[0]}], [{ded2[0]}]\njle {funec}"); break; }
                                }
                                else
                                {
                                    if (inr)
                                    {
                                        File.AppendAllText(file, "\n" + $"\ninc [{ded[0]}]\n cmp [{ded[0]}], {ded2[0]}\njle {funec}"); break;
                                    }
                                    else { File.AppendAllText(file, "\n" + $"\ninc [{ded[0]}]\n cmp [{ded[0]}], {ded2[0]}\njle {funec}"); break; }
                                }

                            }
                            if (ded2[1].Replace(" ", null) == "greater,reg")
                            {

                                if (Char.IsLetter(ded[1][0]))
                                {
                                    if (inr)
                                    {
                                        File.AppendAllText(file, "\n" + $"\ndec {ded[0]}\n cmp {ded[0]}, [{ded2[0]}]\njge {funec}"); break;
                                    }
                                    else { File.AppendAllText(file, "\n" + $"\ndec {ded[0]}\n cmp {ded[0]}, [{ded2[0]}]\njge {funec}"); break; }
                                }
                                else
                                {
                                    if (inr)
                                    {
                                        File.AppendAllText(file, "\n" + $"\ndec {ded[0]}\n cmp {ded[0]}, {ded2[0]}\njge {funec}"); break;
                                    }
                                    else { File.AppendAllText(file, "\n" + $"\ndec {ded[0]}\n cmp {ded[0]}, {ded2[0]}\njge {funec}"); break; }
                                }
                            }
                            if (ded2[1].Replace(" ", null) == "less,reg")
                            {
                                if (Char.IsLetter(ded[1][0]))
                                {
                                    if (inr)
                                    {
                                        File.AppendAllText(file, "\n" + $"\ninc {ded[0]}\n cmp {ded[0]}, [{ded2[0]}]\njle {funec}"); break;
                                    }
                                    else { File.AppendAllText(file, "\n" + $"\ninc {ded[0]}\n cmp {ded[0]}, [{ded2[0]}]\njle {funec}"); break; }
                                }
                                else
                                {
                                    if (inr)
                                    {
                                        File.AppendAllText(file, "\n" + $"\ninc {ded[0]}\n cmp {ded[0]}, {ded2[0]}\njle {funec}"); break;
                                    }
                                    else { File.AppendAllText(file, "\n" + $"\ninc {ded[0]}\n cmp {ded[0]}, {ded2[0]}\njle {funec}"); break; }
                                }

                            }
                            was1 = true; break;
                            //        }
                            //    }
                        }
                    }
                }
                if (f)
                {
                    //Console.WriteLine(d);

                    ParsLoop(d, poz, file, inputFilePath, func, err);
                }
            }
            catch (Exception)
            {

                Console.WriteLine("Error: Invalid loop block syntax! On line: " + (poz + 1)); err++;

            }

        }
    }
}








//if (command.TrimStart().StartsWith("compare"))
//{
//    string command2 = command.TrimStart().Replace("compare ", "");
//    string isp = null;
//    if (command2.Contains("=") || command2.Contains("!") || command2.Contains(">") || command2.Contains("<"))
//    {
//        if (command2.Contains(">") && !command2.Contains(">="))
//        {
//            string pattern = @"\d+";
//            string[] h = command2.Split("then", 2, StringSplitOptions.RemoveEmptyEntries);
//            string[] partsw = h;
//            string[] nums = partsw[0].Split(">", 2, StringSplitOptions.RemoveEmptyEntries);
//            bool was1 = false;
//              string[] lines = File.ReadAllLines(file);
//            string a = null;
//            for (int i = poz; i >= 0; i--)
//            {
//                foreach (var term in func)
//                {
//                    if (lines[i].StartsWith("func ") && lines[i].Contains(term) && was1 == false)
//                    {
//                        a = lines[i].Replace("func ", "");
//                        a = a.Replace("{", "");
//                        was1 = true;break;
//                    }
//                }
//            }
//            bool containsNumbers = false;
//            try
//            {
//                containsNumbers = Regex.IsMatch(nums[1], pattern);
//            }
//            catch { }
//            bool containsNumbers9 = false;
//            try
//            {
//                containsNumbers = Regex.IsMatch(nums[0], pattern);
//            }
//            catch { }
//            bool sas = false;
//            for (int ia = 0; ia < func.Count; ia++)
//            {
//                string com = command.Replace(" ", null);
//                if (nums[1].Trim().StartsWith(func[ia]))
//                {
//                    if (char.IsLetter(nums[1].Trim()[0]) && char.IsLetter(nums[0].Trim()[0]))
//                    {
//                        File.AppendAllText(file, "\n" + $"call {nums[1]} \n cmp eax,[{nums[0].Replace("while ", "")}] \njg {h[1]}");
//                    }
//                    else
//                    {
//                        File.AppendAllText(file, "\n" + $"call {nums[1]} \n cmp eax,{nums[0].Replace("while ", "")} \njg {h[1]}");
//                    }
//                    sas = true;
//                    break;
//                }
//                if (nums[0].Trim().StartsWith(func[ia]))
//                {
//                    if (char.IsLetter(nums[0].Trim()[0]) && char.IsLetter(nums[1].Trim()[0]))
//                    {
//                        File.AppendAllText(file, "\n" + $"call {nums[0]} \n cmp [{nums[1].Replace("while ", "")}],eax \njg {h[1]}");
//                    }
//                    else
//                    {
//                        File.AppendAllText(file, "\n" + $"call {nums[0]} \n cmp eax,{nums[1].Replace("while ", "")} \njg {h[1]}");
//                    }
//                    sas = true;
//                    break;
//                }
//            }
//            if (!char.IsLetter(nums[0].Trim()[0]) && !char.IsLetter(nums[1].Trim()[0]))
//            {
//                if (sas == false)
//                { File.AppendAllText(file, "\n" + $"cmp {nums[0].Replace("while ", "")}, {nums[1]}\njg {h[1]}"); }
//            }
//            else if (char.IsLetter(nums[0].Trim()[0]) && char.IsLetter(nums[1].Trim()[0]))
//            {
//                if (sas == false)
//                { File.AppendAllText(file, "\n" + $"cmp [{nums[0].Replace("while ", "")}], [{nums[1]}]\njg {h[1]}"); }
//            }
//            else if (!char.IsLetter(nums[0].Trim()[0]) && char.IsLetter(nums[1].Trim()[0]))
//            {
//                if (sas == false)
//                { File.AppendAllText(file, "\n" + $"cmp {nums[0].Replace("while ", "")}, [{nums[1]}]\njg {h[1]}"); }
//            }
//            else if (char.IsLetter(nums[0].Trim()[0]) && !char.IsLetter(nums[1].Trim()[0]))
//            {
//                if (sas == false)
//                { File.AppendAllText(file, "\n" + $"cmp [{nums[0].Replace("while ", "")}], {nums[1]}\njg {h[1]}"); }
//            }
//        }
//        if (command2.Contains(">="))
//        {
//            string pattern = @"\d+";
//            string[] h = command2.Split("then", 2, StringSplitOptions.RemoveEmptyEntries);
//            string[] partsw = h;
//            string[] nums = partsw[0].Split(">=", 2, StringSplitOptions.RemoveEmptyEntries);
//            bool was1 = false;
//              string[] lines = File.ReadAllLines(file);
//            string a = null;
//            for (int i = poz; i >= 0; i--)
//            {
//                foreach (var term in func)
//                {
//                    if (lines[i].StartsWith("func ") && lines[i].Contains(term) && was1 == false)
//                    {
//                        a = lines[i].Replace("func ", "");
//                        a = a.Replace("{", "");
//                        was1 = true;break;
//                    }
//                }
//            }
//            bool containsNumbers = false;
//            try
//            {
//                containsNumbers = Regex.IsMatch(nums[1], pattern);
//            }
//            catch { }
//            bool containsNumbers9 = false;
//            try
//            {
//                containsNumbers = Regex.IsMatch(nums[0], pattern);
//            }
//            catch { }
//            bool sas = false;
//            for (int ia = 0; ia < func.Count; ia++)
//            {
//                string com = command.Replace(" ", null);
//                if (nums[1].Trim().StartsWith(func[ia]))
//                {
//                    if (char.IsLetter(nums[1].Trim()[0]) && char.IsLetter(nums[0].Trim()[0]))
//                    {
//                        File.AppendAllText(file, "\n" + $"call {nums[1]} \n cmp [{nums[0].Replace("while ", "")}],eax \njg {h[1]} \n cmp [{nums[0].Replace("while ", "")}],eax \nje {h[1]}");
//                    }
//                    else
//                    {
//                        File.AppendAllText(file, "\n" + $"call {nums[1]} \n cmp eax,{nums[0].Replace("while ", "")} \njg {h[1]}\n cmp eax,{nums[0].Replace("while ", "")} \nje {h[1]}");
//                    }
//                    sas = true;
//                    break;
//                }
//                if (nums[0].Trim().StartsWith(func[ia]))
//                {
//                    if (char.IsLetter(nums[0].Trim()[0]) && char.IsLetter(nums[1].Trim()[0]))
//                    {
//                        File.AppendAllText(file, "\n" + $"call {nums[0]} \n cmp [{nums[1].Replace("while ", "")}],eax \njg {h[1]}\n cmp [{nums[1].Replace("while ", "")}],eax \nje {h[1]}");
//                    }
//                    else
//                    {
//                        File.AppendAllText(file, "\n" + $"call {nums[0]} \ncmp eax,{nums[1].Replace("while ", "")} \njg {h[1]}\n cmp eax,{nums[1].Replace("while ", "")} \nje {h[1]}");
//                    }
//                    sas = true;
//                    break;
//                }
//            }
//            if (!char.IsLetter(nums[0].Trim()[0]) && !char.IsLetter(nums[1].Trim()[0]))
//            {
//                if (sas == false)
//                { File.AppendAllText(file, "\n" + $"cmp {nums[0].Replace("while ", "")}, {nums[1]}\njg {h[1]}\ncmp {nums[0].Replace("while ", "")}, {nums[1]}\nje {h[1]}"); }
//            }
//            else if (char.IsLetter(nums[0].Trim()[0]) && char.IsLetter(nums[1].Trim()[0]))
//            {
//                if (sas == false)
//                { File.AppendAllText(file, "\n" + $"cmp [{nums[0].Replace("while ", "")}], [{nums[1]}]\njg {h[1]}\ncmp [{nums[0].Replace("while ", "")}], [{nums[1]}]\nje {h[1]}"); }
//            }
//            else if (!char.IsLetter(nums[0].Trim()[0]) && char.IsLetter(nums[1].Trim()[0]))
//            {
//                if (sas == false)
//                { File.AppendAllText(file, "\n" + $"cmp {nums[0].Replace("while ", "")}, [{nums[1]}]\njg {h[1]}\ncmp {nums[0].Replace("while ", "")}, [{nums[1]}]\nje {h[1]}"); }
//            }
//            else if (char.IsLetter(nums[0].Trim()[0]) && !char.IsLetter(nums[1].Trim()[0]))
//            {
//                if (sas == false)
//                { File.AppendAllText(file, "\n" + $"cmp [{nums[0].Replace("while ", "")}], {nums[1]}\njg {h[1]}\ncmp [{nums[0].Replace("while ", "")}], {nums[1]}\nje {h[1]}"); }
//            }
//        }
//        if (command2.Contains("<="))
//        {
//            string pattern = @"\d+";
//            string[] h = command2.Split("then", 2, StringSplitOptions.RemoveEmptyEntries);
//            string[] partsw = h;
//            string[] nums = partsw[0].Split("<=", 2, StringSplitOptions.RemoveEmptyEntries);
//            bool was1 = false;
//              string[] lines = File.ReadAllLines(file);
//            string a = null;
//            for (int i = poz; i >= 0; i--)
//            {
//                foreach (var term in func)
//                {
//                    if (lines[i].StartsWith("func ") && lines[i].Contains(term) && was1 == false)
//                    {
//                        a = lines[i].Replace("func ", "");
//                        a = a.Replace("{", "");
//                        was1 = true;break;
//                    }
//                }
//            }
//            bool containsNumbers = false;
//            try
//            {
//                containsNumbers = Regex.IsMatch(nums[1], pattern);
//            }
//            catch { }
//            bool containsNumbers9 = false;
//            try
//            {
//                containsNumbers = Regex.IsMatch(nums[0], pattern);
//            }
//            catch { }
//            bool sas = false;
//            for (int ia = 0; ia < func.Count; ia++)
//            {
//                string com = command.Replace(" ", null);
//                if (nums[1].Trim().StartsWith(func[ia]))
//                {
//                    if (char.IsLetter(nums[1].Trim()[0]) && char.IsLetter(nums[0].Trim()[0]))
//                    {
//                        File.AppendAllText(file, "\n" + $"call {nums[1]} \n cmp [{nums[0].Replace("while ", "")}],eax \njl {h[1]} \n cmp [{nums[0].Replace("while ", "")}],eax \nje {h[1]}");
//                    }
//                    else
//                    {
//                        File.AppendAllText(file, "\n" + $"call {nums[1]} \n cmp eax,{nums[0].Replace("while ", "")}\njl {h[1]}\n cmp eax,{nums[0].Replace("while ", "")} \nje {h[1]}");
//                    }
//                    sas = true;
//                    break;
//                }
//                if (nums[0].Trim().StartsWith(func[ia]))
//                {
//                    if (char.IsLetter(nums[0].Trim()[0]) && char.IsLetter(nums[1].Trim()[0]))
//                    {
//                        File.AppendAllText(file, "\n" + $"call {nums[0]} \n cmp [{nums[1].Replace("while ", "")}],eax \njl {h[1]}\n cmp [{nums[1].Replace("while ", "")}],eax \nje {h[1]}");
//                    }
//                    else
//                    {
//                        File.AppendAllText(file, "\n" + $"call {nums[0]} \n cmp eax,{nums[1].Replace("while ", "")} \njl {h[1]}\n cmp eax,{nums[1].Replace("while ", "")} \nje {h[1]}");
//                    }
//                    sas = true;
//                    break;
//                }
//            }
//            if (!char.IsLetter(nums[0].Trim()[0]) && !char.IsLetter(nums[1].Trim()[0]))
//            {
//                if (sas == false)
//                { File.AppendAllText(file, "\n" + $"cmp {nums[0].Replace("while ", "")}, {nums[1]}\njl {h[1]}\ncmp {nums[0].Replace("while ", "")}, {nums[1]}\nje {h[1]}"); }
//            }
//            else if (char.IsLetter(nums[0].Trim()[0]) && char.IsLetter(nums[1].Trim()[0]))
//            {
//                if (sas == false)
//                { File.AppendAllText(file, "\n" + $"cmp [{nums[0].Replace("while ", "")}], [{nums[1]}]\njl {h[1]}\ncmp [{nums[0].Replace("while ", "")}], [{nums[1]}]\nje {h[1]}"); }
//            }
//            else if (!char.IsLetter(nums[0].Trim()[0]) && char.IsLetter(nums[1].Trim()[0]))
//            {
//                if (sas == false)
//                { File.AppendAllText(file, "\n" + $"cmp {nums[0].Replace("while ", "")}, [{nums[1]}]\njl {h[1]}\ncmp {nums[0].Replace("while ", "")}, [{nums[1]}]\nje {h[1]}"); }
//            }
//            else if (char.IsLetter(nums[0].Trim()[0]) && !char.IsLetter(nums[1].Trim()[0]))
//            {
//                if (sas == false)
//                { File.AppendAllText(file, "\n" + $"cmp [{nums[0].Replace("while ", "")}], {nums[1]}\njl {h[1]}\ncmp [{nums[0].Replace("while ", "")}], {nums[1]}\nje {h[1]}"); }
//            }
//        }
//        else if (command2.Contains("<") && !command2.Contains("<="))
//        {
//            string pattern = @"\d+";
//            string[] h = command2.Split("then", 2, StringSplitOptions.RemoveEmptyEntries);
//            string[] partsw = h;
//            string[] nums = partsw[0].Split("<", 2, StringSplitOptions.RemoveEmptyEntries);
//            bool was1 = false;
//              string[] lines = File.ReadAllLines(file);
//            string a = null;
//            for (int i = poz; i >= 0; i--)
//            {
//                foreach (var term in func)
//                {
//                    if (lines[i].StartsWith("func ") && lines[i].Contains(term) && was1 == false)
//                    {
//                        a = lines[i].Replace("func ", "");
//                        a = a.Replace("{", "");
//                        was1 = true;break;
//                    }
//                }
//            }
//            bool containsNumbers = false;
//            try
//            {
//                containsNumbers = Regex.IsMatch(nums[1], pattern);
//            }
//            catch { }
//            bool containsNumbers9 = false;
//            try
//            {
//                containsNumbers = Regex.IsMatch(nums[0], pattern);
//            }
//            catch { }
//            bool sas = false;
//            for (int ia = 0; ia < func.Count; ia++)
//            {
//                string com = command.Replace(" ", null);
//                if (nums[1].Trim().StartsWith(func[ia]))
//                {
//                    if (char.IsLetter(nums[1].Trim()[0]) && char.IsLetter(nums[0][0]))
//                    {
//                        File.AppendAllText(file, "\n" + $"call {nums[1]} \n cmp [{nums[0].Replace("while ", "")}],eax \njl {h[1]}");
//                    }
//                    else
//                    {
//                        File.AppendAllText(file, "\n" + $"call {nums[1]} \n cmp eax,{nums[0].Replace("while ", "")} \njl {h[1]}");
//                    }
//                    sas = true;
//                    break;
//                }
//                if (nums[0].Trim().StartsWith(func[ia]))
//                {
//                    if (char.IsLetter(nums[0].Trim()[0]) && char.IsLetter(nums[1][0]))
//                    {
//                        File.AppendAllText(file, "\n" + $"call {nums[0]} \n cmp [{nums[1].Replace("while ", "")}],eax \njl {h[1]}");
//                       // //Console.WriteLine(partsw[0]);
//                    }
//                    else
//                    {
//                        File.AppendAllText(file, "\n" + $"call {nums[0]} \n cmp eax,{nums[1].Replace("while ", "")} \njl {h[1]}");
//                    }
//                    sas = true;
//                    break;
//                }
//            }
//            if (!char.IsLetter(nums[0].Trim()[0]) && !char.IsLetter(nums[1].Trim()[0]))
//            {
//                if (sas == false)
//                { File.AppendAllText(file, "\n" + $"cmp {nums[0].Replace("while ", "")}, {nums[1]}\njl {h[1]}"); }
//            }
//            else if (char.IsLetter(nums[0].Trim()[0]) && char.IsLetter(nums[1].Trim()[0]))
//            {
//                if (sas == false)
//                { File.AppendAllText(file, "\n" + $"cmp [{nums[0].Replace("while ", "")}], [{nums[1]}]\njl {h[1]}"); }
//            }
//            else if (!char.IsLetter(nums[0].Trim()[0]) && char.IsLetter(nums[1].Trim()[0]))
//            {
//                if (sas == false)
//                { File.AppendAllText(file, "\n" + $"cmp {nums[0].Replace("while ", "")}, [{nums[1]}]\njl {h[1]}"); }
//            }
//            else if (char.IsLetter(nums[0].Trim()[0]) && !char.IsLetter(nums[1].Trim()[0]))
//            {
//                if (sas == false)
//                { File.AppendAllText(file, "\n" + $"cmp [{nums[0].Replace("while ", "")}], {nums[1]}\njl {h[1]}"); }
//            }
//        }
//        else if (command2.Contains("==") && !command2.Contains("!"))
//        {
//            string pattern = @"\d+";
//            string[] h = command2.Split("then", 2, StringSplitOptions.RemoveEmptyEntries);
//            string[] partsw = h;
//            string[] nums = partsw[0].Split('=', 2);
//            nums[1] = nums[1].Replace("=", "");
//            bool was1 = false;
//              string[] lines = File.ReadAllLines(file);
//            string a = null;
//            for (int i = poz; i >= 0; i--)
//            {
//                foreach (var term in func)
//                {
//                    if (lines[i].StartsWith("func ") && lines[i].Contains(term) && was1 == false)
//                    {
//                        a = lines[i].Replace("func ", "");
//                        a = a.Replace("{", "");
//                        was1 = true;break;
//                    }
//                }
//            }
//            bool containsNumbers = false;
//            try
//            {
//                containsNumbers = Regex.IsMatch(nums[1], pattern);
//            }
//            catch { }
//            bool containsNumbers9 = false;
//            try
//            {
//                containsNumbers = Regex.IsMatch(nums[0], pattern);
//            }
//            catch { }
//            bool sas = false;
//            for (int ia = 0; ia < func.Count; ia++)
//            {
//                string com = command.Replace(" ", null);
//                if (nums[1].Trim().StartsWith(func[ia]))
//                {
//                    if (char.IsLetter(nums[1].Trim()[0]) && char.IsLetter(nums[0][0]))
//                    {
//                        File.AppendAllText(file, "\n" + $"call {nums[1]} \n cmp [{nums[0].Replace("while ", "")}],eax \nje {h[1]}");
//                    }
//                    else
//                    {
//                        File.AppendAllText(file, "\n" + $"call {nums[1]} \n cmp eax,{nums[0].Replace("while ", "")} \nje {h[1]}");
//                    }
//                    sas = true;
//                    break;
//                }
//                if (nums[0].Trim().StartsWith(func[ia]))
//                {
//                    if (char.IsLetter(nums[0].Trim()[0]) && char.IsLetter(nums[1][0]))
//                    {
//                        File.AppendAllText(file, "\n" + $"call {nums[0]} \n cmp [{nums[1].Replace("while ", "")}],eax \nje {h[1]}");
//                        //Console.WriteLine(partsw[0]);
//                    }
//                    else
//                    {
//                        File.AppendAllText(file, "\n" + $"call {nums[0]} \n cmp eax,{nums[1].Replace("while ", "")} \nje {h[1]}");
//                    }
//                    sas = true;
//                    break;
//                }
//            }
//            if (!char.IsLetter(nums[0].Trim()[0]) && !char.IsLetter(nums[1].Trim()[0]))
//            {
//                if (sas == false)
//                { File.AppendAllText(file, "\n" + $"cmp {nums[0].Replace("while ", "")}, {nums[1]}\nje {h[1]}"); }
//            }
//            else if (char.IsLetter(nums[0].Trim()[0]) && char.IsLetter(nums[1].Trim()[0]))
//            {
//                if (sas == false)
//                { File.AppendAllText(file, "\n" + $"cmp [{nums[0].Replace("while ", "")}], [{nums[1]}]\nje {h[1]}"); }
//            }
//            else if (!char.IsLetter(nums[0].Trim()[0]) && char.IsLetter(nums[1].Trim()[0]))
//            {
//                if (sas == false)
//                { File.AppendAllText(file, "\n" + $"cmp {nums[0].Replace("while ", "")}, [{nums[1]}]\nje {h[1]}"); }
//            }
//            else if (char.IsLetter(nums[0].Trim()[0]) && !char.IsLetter(nums[1].Trim()[0]))
//            {
//                if (sas == false)
//                { File.AppendAllText(file, "\n" + $"cmp [{nums[0].Replace("while ", "")}], {nums[1]}\nje {h[1]}"); }
//            }
//        }
//        else if (command2.Contains("!"))
//        {
//            string pattern = @"\d+";
//            string[] h = command2.Split("then", 2, StringSplitOptions.RemoveEmptyEntries);
//            string[] partsw = h;
//            string[] nums = partsw[0].Split("!=", 2, StringSplitOptions.RemoveEmptyEntries);
//            bool was1 = false;
//              string[] lines = File.ReadAllLines(file);
//            string a = null;
//            for (int i = poz; i >= 0; i--)
//            {
//                foreach (var term in func)
//                {
//                    if (lines[i].StartsWith("func ") && lines[i].Contains(term) && was1 == false)
//                    {
//                        a = lines[i].Replace("func ", "");
//                        a = a.Replace("{", "");
//                        was1 = true;break;
//                    }
//                }
//            }
//            bool containsNumbers = false;
//            try
//            {
//                containsNumbers = Regex.IsMatch(nums[1], pattern);
//            }
//            catch { }
//            bool containsNumbers9 = false;
//            try
//            {
//                containsNumbers = Regex.IsMatch(nums[0], pattern);
//            }
//            catch { }
//            bool sas = false;
//            for (int ia = 0; ia < func.Count; ia++)
//            {
//                string com = command.Replace(" ", null);
//                if (nums[1].Trim().StartsWith(func[ia]))
//                {
//                    if (char.IsLetter(nums[1].Trim()[0]) && char.IsLetter(nums[0][0]))
//                    {
//                        File.AppendAllText(file, "\n" + $"call {nums[1]} \n cmp [{nums[0].Replace("while ", "")}],eax \njne {h[1]}");
//                    }
//                    else
//                    {
//                        File.AppendAllText(file, "\n" + $"call {nums[1]} \n cmp eax,{nums[0].Replace("while ", "")} \njne {h[1]}");
//                    }
//                    sas = true;
//                    break;
//                }
//                if (nums[0].Trim().StartsWith(func[ia]))
//                {
//                    if (char.IsLetter(nums[0].Trim()[0]) && char.IsLetter(nums[1][0]))
//                    {
//                        File.AppendAllText(file, "\n" + $"call {nums[0]} \n cmp [{nums[1].Replace("while ", "")}],eax \njne {h[1]}");
//                       // //Console.WriteLine(partsw[0]);
//                    }
//                    else
//                    {
//                        File.AppendAllText(file, "\n" + $"call {nums[0]} \n cmp eax,{nums[1].Replace("while ", "")} \njne {h[1]}");
//                    }
//                    sas = true;
//                    break;
//                }
//            }
//            if (!char.IsLetter(nums[0].Trim()[0]) && !char.IsLetter(nums[1].Trim()[0]))
//            {
//                if (sas == false)
//                { File.AppendAllText(file, "\n" + $"cmp {nums[0].Replace("while ", "")}, {nums[1]}\njne {h[1]}"); }
//            }
//            else if (char.IsLetter(nums[0].Trim()[0]) && char.IsLetter(nums[1].Trim()[0]))
//            {
//                if (sas == false)
//                { File.AppendAllText(file, "\n" + $"cmp [{nums[0].Replace("while ", "")}], [{nums[1]}]\njne {h[1]}"); }
//            }
//            else if (!char.IsLetter(nums[0].Trim()[0]) && char.IsLetter(nums[1].Trim()[0]))
//            {
//                if (sas == false)
//                { File.AppendAllText(file, "\n" + $"cmp {nums[0].Replace("while ", "")}, [{nums[1]}]\njne {h[1]}"); }
//            }
//            else if (char.IsLetter(nums[0].Trim()[0]) && !char.IsLetter(nums[1].Trim()[0]))
//            {
//                if (sas == false)
//                { File.AppendAllText(file, "\n" + $"cmp [{nums[0].Replace("while ", "")}], {nums[1]}\njne {h[1]}"); }
//            }
//        }
//    }
//}