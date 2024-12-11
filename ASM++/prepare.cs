namespace Lumin
{
    public unsafe static class prepare
    {

        public static void Prepare(string file, bool boot, bool fifboot, string mode)
        {
            File.AppendAllText(file, "\nendOasm:\njmp $");

            if (boot && fifboot) { File.AppendAllText(file, "\n" + $"times(512-2-($-07C00h)) db 0\r\ndb 055h,0AAh "); }
            string filePath = file;
            string[] string4 = File.ReadAllLines(filePath);
            List<string> lines5 = string4.Where(item => !string.IsNullOrWhiteSpace(item)).ToList();
            bool iny = false;
            bool wased = false;
            bool iny2 = false;
            bool wased2 = false;
            bool iny3 = false;
            bool wased3 = false;
            bool iny4 = false;
            bool wased4 = false;
            bool iny5 = false;
            bool wased5 = false;
            int index = 0;
            string name = "";
            bool ifed = false;
            bool inmacro = false;
            bool ismain = false;
            List<string> preor = new List<string>();
            //for (int i2 = 0; i2 < lines5.Count(); i2++)
            //{
            //    if (lines5[i2].TrimStart().StartsWith(";Import state is not state"))
            //    {
            //        index = i2;
            //        break;
            //    }

            //}

            for (int i2 = index; i2 < lines5.Count() + index; i2++)
            {



                if (iny4 && lines5[i2].TrimStart().StartsWith("jmp endOasm")) { iny4 = false; }
                if (!iny4 && wased4 && preor[preor.Count() - 1] == "local")
                {
                    lines5[i2] = "\nendl";
                    wased4 = false;
                    preor.RemoveAt(preor.Count() - 1);  
                }
                if (iny5 && lines5[i2].TrimStart().StartsWith("jmp endOasm")) { iny5 = false; }
                if (!iny5 && wased5 && preor[preor.Count() - 1] == "macro")
                {
                    lines5[i2] = "\n}";
                    wased5 = false; preor.RemoveAt(preor.Count() - 1);  
                }


                if (iny2 && lines5[i2].TrimStart().StartsWith("jmp endOasm") || lines5[i2].TrimStart().StartsWith("\njmp endOasm")) { iny2 = false; }
                if (!iny2 && wased2 && preor[preor.Count() - 1] == "proc")
                {
                    if (ismain) lines5[i2] = "\nret\nendp";
                    else lines5[i2] = "\nret\nendp";

                    wased2 = false;
                    inmacro = false; preor.RemoveAt(preor.Count() - 1);  
                }


                //if (iny3 && lines5[i2].TrimStart().StartsWith("jmp endOasm")) { iny3 = false; }
                //if (!iny3 && wased3)
                //{

                //    lines5[i2] = "\nend if";


                //    string com = name;
                //    string macro = null;

                //    if (com.TrimStart().Contains(" "))
                //    {
                //        macro = com.TrimStart().Substring(6);
                //        string[] e = macro.Split(' ', 2);
                //        macro = e[0];
                //        com = com;

                //        com = macro;

                //    }
                //    if (com.StartsWith("eriyupynny"))
                //    {
                //        lines5[i2 + 2] = "\n" + com + "\n" + lines5[i2 + 2];
                //    }

                //    wased3 = false;


                //    ifed = true;

                //}
                if (lines5[i2].TrimStart().StartsWith(".else"))
                {
                    int u = i2;
                    while (lines5[u]!=".endif")
                    {
                        u--;
                    }
                    lines5[u] = "";  
                }



                // Console.WriteLine(i2);
                lines5[i2] = helpfunc.RemovePd(lines5[i2].Trim());
                // lines5[i2] = Removecav(lines5[i2].Trim());

                ////дредкомпилиционный иф
                //if (lines5[i2].TrimStart().StartsWith("if") || lines5[i2].TrimStart().StartsWith("else"))
                //{
                //    iny3 = true;
                //    wased3 = true;
                //}


                //if (lines5[i2].TrimStart().StartsWith("else"))
                //{



                //    if (lines5[i2 - 1].StartsWith("\nend"))
                //    {
                //        lines5[i2 - 1] = "";
                //        Console.WriteLine("Line updated to 't'.");
                //    }

                //}
                if (lines5[i2].TrimStart().StartsWith("struc"))
                {
                    iny = true;
                    wased = true;
                    if (!lines5[i2 + 1].Trim().StartsWith("\n{"))
                    {
                        lines5[i2] += "\n{";  
                    }
                }

                if (iny && lines5[i2].TrimStart().StartsWith("jmp endOasm")) { iny = false; }
                if (!iny && wased)
                {
                    lines5[i2] = "\n}";
                    wased = false;  
                }


                if (lines5[i2].TrimStart().StartsWith("macro"))
                {
                    iny5 = true;
                    wased5 = true;
                    preor.Add("macro");
                    if (!lines5[i2 + 1].Trim().StartsWith("\n{"))
                    {
                        lines5[i2] += "\n{";
                    }
                     
                }


                // if (mode =="16") { lines5[i2] = lines5[i2].Replace("ebp","bp").Replace("esp", "sp").Replace("PROC32.INC","PROC16.INC"); }
                if (mode == "16")
                {

                    lines5[i2] = helpfunc.ReplaceRegisters(lines5[i2].Replace("include 'fasm\\INCLUDE\\MACRO\\PROC32.INC'", "include 'fasm\\INCLUDE\\MACRO\\PROC16.INC'"));

                }
                if (mode == "64")
                {

                    lines5[i2] = helpfunc.ReplaceRegisters2(lines5[i2].Replace("include 'fasm\\INCLUDE\\MACRO\\PROC32.INC'", "include 'fasm\\INCLUDE\\MACRO\\PROC64.INC'").Replace("include 'fasm\\INCLUDE\\MACRO\\import32.INC'", "include 'fasm\\INCLUDE\\MACRO\\import64.INC'"));

                }
                if (lines5[i2].TrimStart().StartsWith("locals"))
                {
                    iny4 = true;
                    wased4 = true;
                    preor.Add("local");
                     
                }


                if (lines5[i2].TrimStart().StartsWith("proc") && !lines5[i2].TrimStart().StartsWith("macro wingui.createwindow"))
                {
                    preor.Add("proc");
                    if (lines5[i2].Trim().Replace(" ", null) == "procmain")
                    {
                        ismain = true;
                    }
                    else { ismain = false; }
                    iny2 = true;
                    wased2 = true;
                    name = lines5[i2];
                    inmacro = true;
                    // Console.WriteLine("DSADASDDDDDDDDDDDDDDDDDDDD");
                    if (!lines5[i2 + 1].Trim().StartsWith("{"))
                    {

                    }


                }





            }

            //  File.WriteAllLines(filePath, lines5);

            List<string> lines4 = lines5;
            bool foundClosingBrace = false;
            bool foundClosingBrace2 = false;
            bool clased = false;

            for (int i = 0; i < lines4.Count() - 1; i++)
            {
                if (string.IsNullOrEmpty(lines4[i].Trim()) || string.IsNullOrWhiteSpace(lines4[i].Trim())) { lines4.RemoveAt(i); }
                if (lines4[i].Trim().Replace(" ", null).Contains("@]"))
                {
                    lines4[i] = helpfunc.RemoveBracketsprep(lines4[i].Trim());
                }
                if (lines4[i].Trim().Replace(" ", null).Contains("[") && lines4[i].Trim().Replace(" ", null).Contains("]"))
                {
                    lines4[i] = helpfunc.Removecav(lines4[i].Trim());
                }
                if (lines4[i].Trim().Replace(" ", null).Contains("@]"))
                {
                    lines4[i] = helpfunc.RemoveBracketsprep(lines4[i].Trim());
                }
                if (lines4[i].Trim().Replace(" ", null).Contains("[") && lines4[i].Trim().Replace(" ", null).Contains("]"))
                {
                    lines4[i] = helpfunc.Removecav(lines4[i].Trim());
                }
                if (lines4[i].TrimStart().StartsWith("struc"))
                {
                    foundClosingBrace = true;
                     
                }

                if (lines4[i].TrimStart().StartsWith("}"))
                {

                    foundClosingBrace = false;
                }

                if (foundClosingBrace && !lines4[i].TrimStart().StartsWith("struc"))
                {
                    if (lines4[i].Contains(" db") || lines4[i].Contains(" dd") || lines4[i].Contains(" dw") || lines4[i].Contains(" dt") || lines4[i].Contains(" dq") || lines4[i].Contains(":") || lines4[i].Contains("proc"))
                    {

                        if (lines4[i].TrimStart().StartsWith("proc ")) { string j = lines4[i].TrimStart().Insert(5, "."); lines4[i] = j; }
                        else
                        {
                            string j = '.' + lines4[i];
                            lines4[i] = j;
                        }
                    }


                }
            }
            for (int i = 0; i < lines4.Count() - 1; i++)
            {
                if (string.IsNullOrEmpty(lines4[i].Trim()) || string.IsNullOrWhiteSpace(lines4[i].Trim())) { lines4.RemoveAt(i); }
            }



            File.WriteAllLines(filePath, lines4);




        }

    }
}
