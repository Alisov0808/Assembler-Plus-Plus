namespace Lumin
{
    public unsafe static class other
    {

        static public List<string> define = new List<string>();
        static public List<string> watdefine = new List<string>();
        static public List<string> definestack = new List<string>();
        static public List<string> watdefinestack = new List<string>();
        public static void Pars(List<string> commands, string inputFilePath, List<string> func, int poz, string file, List<string> struc, bool inClass, string cls, List<string> outputLines, string className, bool inStr, List<string> macroses, List<string> whatin, List<string> classn, int braceCount, List<int> pointof, List<string> peremen, List<string> typeperemen, int errorcount, bool instrict, int laststack)
        {
            try
            {



                int count = commands.Count();

                List<string> result = new List<string>();

                foreach (var command in commands)
                {

                    var parsedResults
                    = helpfunc.ParseCommands(command);
                    result.AddRange(parsedResults);
                }
                commands = result;
                for (int i = 0; i < count; i++)
                {
                    commands[i] = helpfunc.TrimOutsideQuotes(commands[i]);
                    if (commands[i].TrimStart().StartsWith("template"))
                    {

                        className = commands[i].Trim().Substring(8).Trim();
                        cls = className.Replace(" ", null);

                        classn.Add(className.Trim());


                        //  //Console.WriteLine($";classf {className} starts");
                    }

                    count = result.Count();
                    ////// скобочки это вапапвпвкпвкпв
                    if (commands[i].Trim().EndsWith("}"))
                    {

                        commands[i] = commands[i].TrimEnd().Substring(0, commands[i].Length - 1).Trim();
                        commands.Insert(i, "}");


                    }
                    if (commands[i].Trim().Contains("{"))
                    {

                        commands[i] = commands[i].TrimEnd().Substring(0, commands[i].Length - 1).Trim();
                        commands.Insert(i, "{");
                        //}

                    }
                    if (commands[i].TrimStart().StartsWith("define "))
                    {

                        string res = commands[i].TrimStart().Substring(7);
                        string[] a = res.Split(',', 2);

                        define.Add(a[0]);
                        watdefine.Add(a[1]);
                    }
                    if (commands[i].TrimStart().StartsWith("dword") || commands[i].TrimStart().StartsWith("stack ") || commands[i].TrimStart().StartsWith("word") || commands[i].TrimStart().StartsWith("tword") || commands[i].TrimStart().StartsWith("qword") || commands[i].TrimStart().StartsWith("byte") || commands[i].TrimStart().StartsWith("ubyte"))
                    {
                        types.ParsTypesnone(commands[i], file, peremen, typeperemen, func, macroses, errorcount, errorcount, laststack, instrict);
                        
                    }
                    else if (commands[i].TrimStart().StartsWith("proc "))
                    {

                        //  File.AppendAllText(file, $"\n {command3[i].Replace("dword ", null).Replace("word ", null).Replace("qword ", null).Replace("tword ", null).Replace("byte", null).Replace("(", " ").Replace(")", null)}");
                        string[] spl = commands[i].TrimStart().Split("proc ", 2);
                        if (!commands[i].Contains("("))
                        {
                            string[] macro = spl[1].Split(' ', 2);

                            func.Add($"{macro[0].Trim()}");
                        }
                        else
                        {
                            string[] macro = spl[1].Split('(', 2);

                            func.Add($"{macro[0].Trim()}");
                        }
                        //Console.WriteLine($"{macro[0].Trim()}" + "fdfsdfsdfsdfsggggggggggggggggggggggggggggggggggggggggggggggggggggggd");
                    }

                    else if (commands[i].TrimStart().StartsWith("macro "))
                    {

                        //  File.AppendAllText(file, $"\n {command3[i].Replace("dword ", null).Replace("word ", null).Replace("qword ", null).Replace("tword ", null).Replace("byte", null).Replace("(", " ").Replace(")", null)}");
                        string[] spl = commands[i].TrimStart().Split("macro ", 2);
                        if (!commands[i].Contains("("))
                        {
                            string[] macro = spl[1].Split(' ', 2);

                            macroses.Add($"{macro[0].Trim()}");
                        }
                        else
                        {
                            string[] macro = spl[1].Split('(', 2);

                            macroses.Add($"{macro[0].Trim()}");
                        }
                        //Console.WriteLine($"{macro[0].Trim()}" + "fdfsdfsdfsdfsggggggggggggggggggggggggggggggggggggggggggggggggggggggd");
                    }
                    //if (commands[i].TrimStart().StartsWith("proc "))
                    //{


                    //    string[] spl = commands[i].Replace("dword ", null).Replace("word ", null).Split("proc", 2);
                    //    string[] macro = spl[1].Split('(');
                    //    func.Add($"{macro[0].Trim()}");
                    //     Console.WriteLine($"{macro[0].Trim()}" + "fdfsdfsdfsdfsd");
                    //}
                    //    else if (commands[i].TrimStart().StartsWith("macro "))
                    //{


                    //    string[] spl = commands[i].Replace("dword ", null).Replace("word ", null).Split("macro", 2);
                    //    string[] macro = spl[1].Split('(');
                    //    macroses.Add($"{macro[0].Trim()}");

                    //}
                    else if (commands[i].TrimStart().StartsWith("func "))
                    {
                        string command2 = commands[i].Replace("func ", "");
                        string isp = null;
                        if (command2.Contains("{")) { command2 = command2.Replace("{", ""); }
                        func.Add(command2.Replace(" ", "").Trim());

                    }


                }

                for (int i = 0; i < commands.Count(); i++)
                {


                    commands[i] = helpfunc.TrimOutsideQuotes(commands[i].TrimEnd());
                    if (commands[i].TrimEnd().EndsWith(";"))
                    {
                        try
                        {
                            string[] h = commands[i].TrimEnd().Split(commands[i].TrimEnd()[commands[i].TrimEnd().Length], 2);
                            commands.Insert(i, h[0]);
                            count = commands.Count();

                        }
                        catch { }
                    }
                    if (commands[i].TrimEnd().EndsWith(")") && !commands[i].TrimStart().StartsWith("crttemp ") && !commands[i].TrimEnd().EndsWith("dword)") && !commands[i].TrimEnd().EndsWith("word)") && !commands[i].TrimEnd().EndsWith("tword)") && !commands[i].TrimEnd().EndsWith("qword)") && !commands[i].TrimEnd().EndsWith("byte)") && !commands[i].TrimEnd().EndsWith("ubyte)"))
                    {
                        bool isit = false;
                        string[] a = commands[i].Trim().Split("(", 2);
                        for (int i2 = 0; i2 < classn.Count - 1; i2++)
                        {
                            if (a[0].Replace(" ", null) == classn[i])
                            {
                                isit = true;
                            }
                        }
                        if (!isit)
                        {
                            commands[i] = commands[i].TrimEnd().TrimStart().Substring(0, commands[i].TrimEnd().TrimStart().Length - 1);
                            int ig = commands[i].IndexOf("(");
                            commands[i] = commands[i].Remove(ig, 1).Insert(ig, " ");
                        }

                        // outputLines.Add(commands[i]);

                    }

                    if (commands[i].TrimStart().StartsWith("struct"))
                    {
                        //try
                        //{

                        //commands[i] = commands[i].TrimEnd().TrimStart().Substring(0, commands[i].TrimEnd().TrimStart().Length - 1);
                        //int ig = commands[i].IndexOf("(");
                        //commands[i] = commands[i].Remove(ig, 1).Insert(ig, " ");
                        //}
                        //catch (Exception)
                        //{


                        //}
                        commands[i] = commands[i].Replace(")", " ").Replace("(", null);
                        var lines = File.ReadAllLines(inputFilePath);
                        string isp = null;
                        string[] s = commands[i].TrimStart().Split(' ');
                        struc.Add(s[1].Replace(" ", "").Trim());
                        ////Console.WriteLine("dsadasdaaaaaaaaaaaa   " + s[1].Replace(" ", "").Trim());
                    }

                    if (commands[i].TrimStart().StartsWith("func"))
                    {
                        string command2 = commands[i].Replace("func ", "");
                        string isp = null;
                        if (command2.Contains("{")) { command2 = command2.Replace("{", ""); }
                        int currentPosition = poz;
                        string fileContent = File.ReadAllText(file);
                        string[] lines2 = fileContent.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
                        func.Add(command2.Replace(" ", "").Trim());
                    }



                    string currentCommand = commands[i];
                    var trimmedLine = currentCommand.TrimStart();
                    string line = currentCommand;




                    if (inClass && trimmedLine.TrimStart().Contains("."))
                    {
                        bool inQuotes = false;
                        char quoteChar = '\0';


                        for (int il = 0; il < trimmedLine.Length; il++)
                        {

                            if (trimmedLine[il] == '"' || trimmedLine[il] == '\'')
                            {
                                if (inQuotes)
                                {
                                    if (trimmedLine[il] == quoteChar)
                                    {

                                        inQuotes = false;
                                    }
                                }
                                else
                                {

                                    inQuotes = true;
                                    quoteChar = trimmedLine[i];
                                }
                            }

                            try
                            {



                                if (trimmedLine[il] == '.' && !inQuotes)
                                {

                                    trimmedLine = trimmedLine.Insert(il, cls);

                                    il += cls.Length;
                                    outputLines.Add(trimmedLine);


                                }
                            }
                            catch (Exception)
                            {


                            }
                        }
                    }

                    else if (trimmedLine.TrimStart().StartsWith("template"))
                    {
                        whatin.Add(";class starts");
                        className = trimmedLine.Trim().Substring(8).Trim();
                        cls = className;
                        inClass = true;

                        pointof.Add(i);
                        outputLines.Add($"\n;class {className} starts\n");
                        continue;
                        //  //Console.WriteLine($";classf {className} starts");
                    }

                    else if (trimmedLine.TrimStart().StartsWith("{"))
                    {

                        braceCount++;
                    }
                    else if (trimmedLine.TrimStart().StartsWith("}"))
                    {

                        braceCount--;

                        outputLines.Add(trimmedLine);
                        if (braceCount == 0 && inClass)
                        {
                            inClass = false;
                            ;
                            outputLines.Add($"\n;class ends\n")
                                ;
                            //   //Console.WriteLine($";class ends");

                        }
                        else if (inStr)
                        {
                            inStr = false;
                        }




                    }
                    else if (!inStr && inClass && trimmedLine.TrimStart().StartsWith("stack "))
                    {

                        if (!inStr && inClass && trimmedLine.Trim().Replace(" ", null).StartsWith("stackbyte") && trimmedLine.TrimStart().Contains("="))
                        {

                            outputLines.Add($"\nstack byte {className}.{trimmedLine.Trim().Substring(10).Trim()}");

                            whatin.Add($"\nstack byte {className}.{trimmedLine.Trim().Substring(10).Trim()}");
                        }
                        else if (!inStr && inClass && trimmedLine.Trim().Replace(" ", null).StartsWith("stackubyte") && trimmedLine.TrimStart().Contains("="))
                        {

                            outputLines.Add($"\nstack ubyte {className}.{trimmedLine.Trim().Substring(11).Trim()}");
                            whatin.Add($"\nstack ubyte {className}.{trimmedLine.Trim().Substring(11).Trim()}");
                        }
                        else if (!inStr && inClass && trimmedLine.Trim().Replace(" ", null).StartsWith("stackword") && trimmedLine.TrimStart().Contains("="))
                        {

                            outputLines.Add($"\nstackword {className}.{trimmedLine.Trim().Substring(10).Trim()}");
                            whatin.Add($"\nstack word {className}.{trimmedLine.Trim().Substring(10).Trim()}");
                        }
                        else if (!inStr && inClass && trimmedLine.Trim().Replace(" ", null).StartsWith("stackdword") && trimmedLine.TrimStart().Contains("="))
                        {

                            outputLines.Add($"\nstack dword {className}.{trimmedLine.Trim().Substring(11).Trim()}");
                            whatin.Add($"\nstack dwrd {className}.{trimmedLine.Trim().Substring(11).Trim()}");
                        }
                        else if (!inStr && inClass && trimmedLine.Trim().Replace(" ", null).Replace(" ", null).StartsWith("stacktword") && trimmedLine.TrimStart().Contains("="))
                        {

                            outputLines.Add($"\nstack tword {className}.{trimmedLine.Trim().Substring(11).Trim()}");
                            whatin.Add($"\ntstack word {className}.{trimmedLine.Trim().Substring(11).Trim()}");
                        }
                    }
                    else if (!inStr && inClass && trimmedLine.TrimStart().StartsWith("byte ") && trimmedLine.TrimStart().Contains("="))
                    {

                        outputLines.Add($"\nbyte {className}.{trimmedLine.Trim().Substring(5).Trim()}");
                        whatin.Add($"\nbyte {className}.{trimmedLine.Trim().Substring(5).Trim()}");
                    }
                    else if (!inStr && inClass && trimmedLine.TrimStart().StartsWith("ubyte ") && trimmedLine.TrimStart().Contains("="))
                    {

                        outputLines.Add($"\nubyte {className}.{trimmedLine.Trim().Substring(6).Trim()}");
                        whatin.Add($"\nubyte {className}.{trimmedLine.Trim().Substring(6).Trim()}");
                    }
                    else if (!inStr && inClass && trimmedLine.TrimStart().StartsWith("word ") && trimmedLine.TrimStart().Contains("="))
                    {

                        outputLines.Add($"\nword {className}.{trimmedLine.Trim().Substring(5).Trim()}");
                        whatin.Add($"\nword {className}.{trimmedLine.Trim().Substring(5).Trim()}");
                    }
                    else if (!inStr && inClass && trimmedLine.TrimStart().StartsWith("dword ") && trimmedLine.TrimStart().Contains("="))
                    {

                        outputLines.Add($"\ndword {className}.{trimmedLine.Trim().Substring(5).Trim()}");
                        whatin.Add($"\ndwrd {className}.{trimmedLine.Trim().Substring(5).Trim()}");
                    }
                    else if (!inStr && inClass && trimmedLine.TrimStart().StartsWith("tword ") && trimmedLine.TrimStart().Contains("="))
                    {

                        outputLines.Add($"\ntword {className}.{trimmedLine.Trim().Substring(5).Trim()}");
                        whatin.Add($"\ntword {className}.{trimmedLine.Trim().Substring(5).Trim()}");
                    }
                    else if (!inStr && inClass && trimmedLine.TrimStart().StartsWith("qword ") && trimmedLine.TrimStart().Contains("="))
                    {

                        outputLines.Add($"\nqword {className}.{trimmedLine.Trim().Substring(5).Trim()}");
                        whatin.Add($"\nqword {className}.{trimmedLine.Trim().Substring(5).Trim()}");
                    }
                    else if (inClass && trimmedLine.TrimStart().StartsWith("label "))
                    {
                        var methodName = trimmedLine.Trim().Substring(6).Trim();
                        outputLines.Add($"\nlabel {className}.{methodName}");
                        func.Add($"{className}.{methodName.Replace(":", null)}");

                        whatin.Add($"\n{className}.{methodName}");
                    }
                    else if (inClass && trimmedLine.TrimStart().StartsWith("func ") || inClass && trimmedLine.TrimStart().StartsWith(cls) && !trimmedLine.TrimStart().Contains("proc "))
                    {



                        if (trimmedLine.Trim().StartsWith(cls))
                        {
                            string sp = trimmedLine.TrimStart().Substring(cls.Length + 1);
                            sp = sp.Replace(")", null);
                            outputLines.Add($"\nproc {cls}.constructing {sp}");

                            func.Add($"{cls}.constructing"); ////Console.WriteLine($"{cls}.constructing");

                        }
                        else
                        {
                            var methodName = trimmedLine.Trim().Substring(5).Trim();
                            outputLines.Add($"\nfunc {className}.{methodName}");
                            func.Add($"{className}.{methodName}");
                            whatin.Add($"\nfunc {className}.{methodName}");
                        }


                        //chk = true;
                        continue;

                        ////Console.WriteLine($"{className}.{methodName}");
                    }

                    else if (inClass && trimmedLine.TrimStart().StartsWith("proc "))
                    {

                        var methodName = trimmedLine.TrimStart().Substring(5);
                        outputLines.Add($"\nproc {className}.{methodName}");
                        string[] spl = trimmedLine.Split("proc ", 2);
                        string[] macro = spl[1].Split(' ', 2);
                        func.Add($"{className}.{macro[0].Trim()}");
                        //Console.WriteLine("ddasdadd " + $"{className}.{macro[0].Trim()}");
                        //  whatin.Add($"\nfunc {className}.{methodName}");
                        continue;
                    }
                    else if (inClass && trimmedLine.TrimStart().StartsWith("macro "))
                    {

                        var methodName = trimmedLine.Trim().Substring(6).Trim();
                        outputLines.Add($"\nmacro {className}.{methodName}");
                        string[] spl = trimmedLine.Split("macro ", 2);
                        string[] macro = spl[1].Split(' ');
                        macroses.Add($"{className}.{macro[0].Trim()}");
                        //Console.WriteLine("ddasdadd " + $"{className}.{macro[0].Trim()}");
                        //  whatin.Add($"\nfunc {className}.{methodName}");
                        continue;
                    }
                    else if (inClass && trimmedLine.TrimStart().StartsWith("struct "))
                    {

                        var methodName = trimmedLine.Trim().Substring(7).Trim();
                        outputLines.Add($"\nstruct {className}.{methodName}");
                        whatin.Add($"\n {className}.{methodName}");
                        inStr = true;
                        continue;
                    }
                    else
                    {

                        outputLines.Add(currentCommand);
                    }

                    // if (inClass) { whatin.Add(currentCommand); }

                }
            }
            catch (Exception)
            {


            }


        }
    }
}
