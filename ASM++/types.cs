namespace Lumin
{
    static unsafe public class types
    {public static int cou = 0;
        static public void ParsTypes(string command, string file, List<string> peremen, List<string> typeperemen, List<string> func, List<string> macroses, int poz, int error, int laststack, bool instruct, List<string> dllfunc, List<string> addend, int lasted)
        {
            try
            {
                if (!command.Trim().StartsWith("dword [ebp +"))
                {
                    if (command.TrimStart().StartsWith("dword"))
                    {
                        string[] parts = command.TrimStart().Split(new char[] { ' ' }, 2);
                        if (parts.Length > 1 && parts[1].Contains("="))
                        {
                            string[] a2 = parts[1].Split(new char[] { '=' }, 2).Select(s => s.Trim()).ToArray();
                            if (a2.Length > 1)
                            {
                                if (a2[1] == "null") { File.AppendAllText(file, "\n" + $"{a2[0]} dd ?"); }
                                else
                                {
                                    peremen.Add(a2[0].Trim());
                                    typeperemen.Add("dword");
                                    int isp2 = Parser.isperemen(a2[1].Trim(), peremen, typeperemen);
                                    if (!char.IsNumber(a2[1].Trim()[0]) && !a2[1].Trim().StartsWith("\"") && !a2[1].Trim().Replace(" ", null).StartsWith("'") && !instruct && isp2 == -1)
                                    {
                                        File.AppendAllText(file, "\n" + $"{a2[0]} dd ?");
                                        Parser.Pars(a2[0] + " = " + a2[1], file, func, poz, peremen, typeperemen, macroses, dllfunc, addend, lasted);
                                    }
                                    else { File.AppendAllText(file, "\n" + $"{a2[0]} dd {a2[1].Trim()}"); }
                                }
                            }

                        }
                        else
                        {
                            string[] a2 = parts[1].Split(new char[] { '=' }, 2).Select(s => s.Trim()).ToArray();
                            peremen.Add(a2[0].Trim());
                            typeperemen.Add("dword");
                            File.AppendAllText(file, "\n" + $"{command.TrimStart().Substring(6)} dd ?");
                        }
                    }
                    else if (command.TrimStart().StartsWith("stack"))
                    {
                        string[] parts = command.TrimStart().Split(new char[] { ' ' }, 2);
                        string[] a = parts[1].Split(new char[] { ' ' }, 2).Select(s => s.Trim()).ToArray();
                        string[] a2 = a[1].Split(new char[] { '=' }, 2).Select(s => s.Trim()).ToArray();
                        
                        string wat = "";
                        if (a2[1] == "null") { File.AppendAllText(file, "\n" + $"{a[1]} dd ?"); }
                        else
                        {

                            peremen.Add(a2[0].Trim());
                            typeperemen.Add(a[0].Trim());
                            other.definestack.Add(a2[0]);
                           
                            if (a[0].Trim() == "byte")
                            {
                                File.AppendAllText(file, "\n" + $"\n sub  esp,1");
                                cou += 1;
                                wat = "byte";
                            }
                            if (a[0].Trim() == "ubyte")
                            {
                                File.AppendAllText(file, "\n" + $"\n sub  esp,2");
                                cou += 2;
                            }
                            if (a[0].Trim() == "word")
                            {
                                File.AppendAllText(file, "\n" + $"\n sub  esp,2");
                                cou += 2; wat = "word";
                            }
                            if (a[0].Trim() == "dword")
                            {
                                File.AppendAllText(file, "\n" + $"\n sub  esp,4");
                                cou += 4; wat = "dword";
                            }
                            if (a[0].Trim() == "tword")
                            {
                                File.AppendAllText(file, "\n" + $"\n sub  esp,10");
                                cou += 10; wat = "tword";
                            }
                            if (a[0].Trim() == "qword")
                            {
                                File.AppendAllText(file, "\n" + $"\n sub  esp,8");
                                cou += 8; wat = "qword";
                            }
                          
                            if (a[0].Trim() == "qword")
                            {
                                File.AppendAllText(file, "\n" + $"\n sub  esp,8");
                                cou += 8; wat = "qword";
                            }
                            if (char.IsLetter(a2[1].TrimStart()[0]))
                            {
                                Parser.Pars(a2[0] + " = " + a2[1], file, func, poz, peremen, typeperemen, macroses, dllfunc, addend, lasted);
                            }
                            else {File.AppendAllText(file, $"\n mov {wat} [ebp + {cou}], {a2[1]} "); }
                            other.watdefinestack.Add($"ebp + {cou}");
                        }


                    }
                    else if (command.TrimStart().StartsWith("word"))
                    {
                        string[] parts = command.TrimStart().Split(new char[] { ' ' }, 2);
                        if (parts.Length > 1 && parts[1].Contains("="))
                        {
                            string[] a2 = parts[1].Split(new char[] { '=' }, 2).Select(s => s.Trim()).ToArray();
                            if (a2.Length > 1)
                            {
                                if (a2[1] == "null") { File.AppendAllText(file, "\n" + $"{a2[0]} dw ?"); }
                                else
                                {
                                    peremen.Add(a2[0].Trim());
                                    typeperemen.Add("word");
                                    int isp2 = Parser.isperemen(a2[1].Trim(), peremen, typeperemen);
                                    if (!char.IsNumber(a2[1].Trim()[0]) && !a2[1].Trim().StartsWith("\"") && !a2[1].Trim().Replace(" ", null).StartsWith("'") && !instruct && isp2 == -1)
                                    {
                                        File.AppendAllText(file, "\n" + $"{a2[0]} dw ?");
                                        Parser.Pars(a2[0] + " = " + a2[1], file, func, poz, peremen, typeperemen, macroses, dllfunc, addend, lasted);
                                    }
                                    else { File.AppendAllText(file, "\n" + $"{a2[0]} dw {a2[1].Trim()}"); }
                                }
                            }

                        }
                        else
                        {
                            string[] a2 = parts[1].Split(new char[] { '=' }, 2).Select(s => s.Trim()).ToArray();
                            peremen.Add(a2[0].Trim());
                            typeperemen.Add("dword");
                            File.AppendAllText(file, "\n" + $"{command.TrimStart().Substring(5)} dw ?");
                        }

                    }
                    else if (command.TrimStart().StartsWith("tword"))
                    {
                        string[] parts = command.TrimStart().Split(new char[] { ' ' }, 2);
                        if (parts.Length > 1 && parts[1].Contains("="))
                        {
                            string[] a2 = parts[1].Split(new char[] { '=' }, 2).Select(s => s.Trim()).ToArray();
                            if (a2.Length > 1)
                            {
                                if (a2[1] == "null") { File.AppendAllText(file, "\n" + $"{a2[0]} dt ?"); }
                                else
                                {
                                    peremen.Add(a2[0].Trim());
                                    typeperemen.Add("tword");
                                    int isp2 = Parser.isperemen(a2[1].Trim(), peremen, typeperemen);
                                    if (!char.IsNumber(a2[1].Trim()[0]) && !a2[1].Trim().StartsWith("\"") && !a2[1].Trim().Replace(" ", null).StartsWith("'") && !instruct && isp2 == -1)
                                    {
                                        File.AppendAllText(file, "\n" + $"{a2[0]} dt ?");
                                        Parser.Pars(a2[0] + " = " + a2[1], file, func, poz, peremen, typeperemen, macroses, dllfunc, addend, lasted);
                                    }
                                    else { File.AppendAllText(file, "\n" + $"{a2[0]} dt {a2[1].Trim()}"); }
                                }
                            }

                        }
                        else
                        {
                            string[] a2 = parts[1].Split(new char[] { '=' }, 2).Select(s => s.Trim()).ToArray();
                            peremen.Add(a2[0].Trim());
                            typeperemen.Add("dword");
                            File.AppendAllText(file, "\n" + $"{command.TrimStart().Substring(6)} dt ?");
                        }
                    }
                    else if (command.TrimStart().StartsWith("byte"))
                    {
                        string[] parts = command.TrimStart().Split(new char[] { ' ' }, 2);
                        if (parts.Length > 1 && parts[1].Contains("="))
                        {
                            string[] a2 = parts[1].Split(new char[] { '=' }, 2).Select(s => s.Trim()).ToArray();
                            if (a2.Length > 1)
                            {
                                if (a2[1] == "null") { File.AppendAllText(file, "\n" + $"{a2[0]} db ?"); }
                                else
                                {
                                    peremen.Add(a2[0].Trim());
                                    typeperemen.Add("byte");
                                    int isp2 = Parser.isperemen(a2[1].Trim(), peremen, typeperemen);
                                    if (!char.IsNumber(a2[1].Trim()[0]) && !a2[1].Trim().StartsWith("\"") && !a2[1].Trim().Replace(" ", null).StartsWith("'") && !instruct && isp2 == -1)
                                    {
                                        File.AppendAllText(file, "\n" + $"{a2[0]} db ?");
                                        Parser.Pars(a2[0] + " = " + a2[1], file, func, poz, peremen, typeperemen, macroses, dllfunc, addend, lasted);
                                    }
                                    else { File.AppendAllText(file, "\n" + $"{a2[0]} db {a2[1].Trim()}"); }
                                }
                            }

                        }
                        else
                        {
                            string[] a2 = parts[1].Split(new char[] { '=' }, 2).Select(s => s.Trim()).ToArray();
                            peremen.Add(a2[0].Trim());
                            typeperemen.Add("dword");
                            File.AppendAllText(file, "\n" + $"{command.TrimStart().Substring(5)} db ?");
                        }
                    }
                    else if (command.TrimStart().StartsWith("ubyte"))
                    {
                        string[] parts = command.TrimStart().Split(new char[] { ' ' }, 2);
                        if (parts.Length > 1 && parts[1].Contains("="))
                        {
                            string[] a2 = parts[1].Split(new char[] { '=' }, 2).Select(s => s.Trim()).ToArray();
                            if (a2.Length > 1)
                            {
                                if (a2[1] == "null") { File.AppendAllText(file, "\n" + $"{a2[0]} du ?"); }
                                else
                                {
                                    peremen.Add(a2[0].Trim());
                                    typeperemen.Add("ubyte");

                                    int isp2 = Parser.isperemen(a2[1].Trim(), peremen, typeperemen);
                                    if (!char.IsNumber(a2[1].Trim()[0]) && !a2[1].Trim().StartsWith("\"") && !a2[1].Trim().Replace(" ", null).StartsWith("'") && !instruct && isp2 == -1)
                                    {
                                        File.AppendAllText(file, "\n" + $"{a2[0]} du ?");
                                        Parser.Pars(a2[0] + " = " + a2[1], file, func, poz, peremen, typeperemen, macroses, dllfunc, addend, lasted);
                                    }
                                    else { File.AppendAllText(file, "\n" + $"{a2[0]} du {a2[1].Trim()}"); }
                                }
                            }

                        }
                        else
                        {
                            string[] a2 = parts[1].Split(new char[] { '=' }, 2).Select(s => s.Trim()).ToArray();
                            peremen.Add(a2[0].Trim());
                            typeperemen.Add("dword");
                            File.AppendAllText(file, "\n" + $"{command.TrimStart().Substring(6)} du ?");
                        }
                    }
                    else if (command.TrimStart().StartsWith("qword"))
                    {
                        string[] parts = command.TrimStart().Split(new char[] { ' ' }, 2);
                        if (parts.Length > 1 && parts[1].Contains("="))
                        {
                            string[] a2 = parts[1].Split(new char[] { '=' }, 2).Select(s => s.Trim()).ToArray();
                            if (a2.Length > 1)
                            {
                                if (a2[1] == "null") { File.AppendAllText(file, "\n" + $"{a2[0]} dq ?"); }
                                else
                                {
                                    peremen.Add(a2[0].Trim());
                                    typeperemen.Add("qword");
                                    int isp2 = Parser.isperemen(a2[1].Trim(), peremen, typeperemen);
                                    if (!char.IsNumber(a2[1].Trim()[0]) && !a2[1].Trim().StartsWith("\"") && !a2[1].Trim().Replace(" ", null).StartsWith("'") && !instruct && isp2 == -1)
                                    {
                                        File.AppendAllText(file, "\n" + $"{a2[0]} dq ?");
                                        Parser.Pars(a2[0] + " = " + a2[1], file, func, poz, peremen, typeperemen, macroses, dllfunc, addend, lasted);
                                    }
                                    else { File.AppendAllText(file, "\n" + $"{a2[0]} dq {a2[1].Trim()}"); }
                                }
                            }

                        }
                        else
                        {
                            string[] a2 = parts[1].Split(new char[] { '=' }, 2).Select(s => s.Trim()).ToArray();
                            peremen.Add(a2[0].Trim());
                            typeperemen.Add("dword");
                            File.AppendAllText(file, "\n" + $"{command.TrimStart().Substring(6)} dd ?");
                        }
                    }
                }
                else
                {

                    Sas.Parscall(file, command, func, poz, peremen, typeperemen, macroses, dllfunc, false, true);
                }
            }
            catch { Console.WriteLine("Error: Error in variable syntax! On line: " + (poz + 1)); error++; }
        }

        static public void ParsTypesnone(string command, string file, List<string> peremen, List<string> typeperemen, List<string> func, List<string> macroses, int poz, int error, int laststack, bool instruct)
        {
            try
            {
                if (!command.TrimStart().Substring(command.TrimStart().IndexOf(' ')).Trim().StartsWith("["))
                {
                    if (command.TrimStart().StartsWith("dword"))
                    {
                        string[] parts = command.TrimStart().Split(new char[] { ' ' }, 2);
                        if (parts.Length > 1 && parts[1].Contains("="))
                        {
                            string[] a2 = parts[1].Split(new char[] { '=' }, 2).Select(s => s.Trim()).ToArray();
                            if (a2.Length > 1)
                            {

                                peremen.Add(a2[0].Trim());
                                typeperemen.Add("dword");


                            }

                        }
                        else
                        {
                            string[] a2 = parts[1].Split(new char[] { '=' }, 2).Select(s => s.Trim()).ToArray();
                            peremen.Add(a2[0].Trim());
                            typeperemen.Add("dword");

                        }
                    }
                    else if (command.TrimStart().StartsWith("stack"))
                    {
                        string[] parts = command.TrimStart().Split(new char[] { ' ' }, 2);
                        string[] a = parts[1].Split(new char[] { ' ' }, 2).Select(s => s.Trim()).ToArray();
                        string[] a2 = a[1].Split(new char[] { '=' }, 2).Select(s => s.Trim()).ToArray();
                        string[] a3 = a2[1].Split(',', 2);


                        peremen.Add(a2[0].Trim());
                        typeperemen.Add(a[0].Trim());




                    }
                    else if (command.TrimStart().StartsWith("word"))
                    {
                        string[] parts = command.TrimStart().Split(new char[] { ' ' }, 2);
                        if (parts.Length > 1 && parts[1].Contains("="))
                        {
                            string[] a2 = parts[1].Split(new char[] { '=' }, 2).Select(s => s.Trim()).ToArray();
                            if (a2.Length > 1)
                            {

                                peremen.Add(a2[0].Trim());
                                typeperemen.Add("word");


                            }

                        }
                        else
                        {
                            string[] a2 = parts[1].Split(new char[] { '=' }, 2).Select(s => s.Trim()).ToArray();
                            peremen.Add(a2[0].Trim());
                            typeperemen.Add("dword");

                        }

                    }
                    else if (command.TrimStart().StartsWith("tword"))
                    {
                        string[] parts = command.TrimStart().Split(new char[] { ' ' }, 2);
                        if (parts.Length > 1 && parts[1].Contains("="))
                        {
                            string[] a2 = parts[1].Split(new char[] { '=' }, 2).Select(s => s.Trim()).ToArray();
                            if (a2.Length > 1)
                            {
                                if (a2[1] == "null")
                                { //File.AppendAllText(file, "\n" + $"{a2[0]} dt ?");
                                }
                                else
                                {
                                    peremen.Add(a2[0].Trim());
                                    typeperemen.Add("tword");
                                    if (!char.IsNumber(a2[1].Trim()[0]) && !a2[1].Trim().StartsWith("\"") && !a2[1].Trim().Replace(" ", null).StartsWith("'") && !instruct)
                                    {
                                        //File.AppendAllText(file, "\n" + $"{a2[0]} dt ?");
                                        //Parser.Pars(a2[0] + " = " + a2[1], file, func, poz, peremen, typeperemen, macroses);
                                    }
                                    else
                                    { //File.AppendAllText(file, "\n" + $"{a2[0]} dt {a2[1].Trim()}");
                                    }
                                }
                            }

                        }
                        else
                        {
                            string[] a2 = parts[1].Split(new char[] { '=' }, 2).Select(s => s.Trim()).ToArray();
                            peremen.Add(a2[0].Trim());
                            typeperemen.Add("dword");
                            //File.AppendAllText(file, "\n" + $"{command.TrimStart().Substring(6)} dt &");
                        }
                    }
                    else if (command.TrimStart().StartsWith("byte"))
                    {
                        string[] parts = command.TrimStart().Split(new char[] { ' ' }, 2);
                        if (parts.Length > 1 && parts[1].Contains("="))
                        {
                            string[] a2 = parts[1].Split(new char[] { '=' }, 2).Select(s => s.Trim()).ToArray();
                            if (a2.Length > 1)
                            {
                                if (a2[1] == "null")
                                { //File.AppendAllText(file, "\n" + $"{a2[0]} db ?");
                                }
                                else
                                {
                                    peremen.Add(a2[0].Trim());
                                    typeperemen.Add("byte");
                                    if (!char.IsNumber(a2[1].Trim()[0]) && !a2[1].Trim().StartsWith("\"") && !a2[1].Trim().Replace(" ", null).StartsWith("'") && !instruct)
                                    {
                                        //File.AppendAllText(file, "\n" + $"{a2[0]} db ?");
                                        //Parser.Pars(a2[0] + " = " + a2[1], file, func, poz, peremen, typeperemen, macroses);
                                    }
                                    else
                                    { //File.AppendAllText(file, "\n" + $"{a2[0]} db {a2[1].Trim()}");
                                    }
                                }
                            }

                        }
                        else
                        {
                            string[] a2 = parts[1].Split(new char[] { '=' }, 2).Select(s => s.Trim()).ToArray();
                            peremen.Add(a2[0].Trim());
                            typeperemen.Add("dword");
                            //File.AppendAllText(file, "\n" + $"{command.TrimStart().Substring(5)} db ?");
                        }
                    }
                    else if (command.TrimStart().StartsWith("ubyte"))
                    {
                        string[] parts = command.TrimStart().Split(new char[] { ' ' }, 2);
                        if (parts.Length > 1 && parts[1].Contains("="))
                        {
                            string[] a2 = parts[1].Split(new char[] { '=' }, 2).Select(s => s.Trim()).ToArray();
                            if (a2.Length > 1)
                            {
                                if (a2[1] == "null")
                                { //File.AppendAllText(file, "\n" + $"{a2[0]} du ?");
                                }
                                else
                                {
                                    peremen.Add(a2[0].Trim());
                                    typeperemen.Add("ubyte");


                                    if (!char.IsNumber(a2[1].Trim()[0]) && !a2[1].Trim().StartsWith("\"") && !a2[1].Trim().Replace(" ", null).StartsWith("'") && !instruct)
                                    {
                                        //File.AppendAllText(file, "\n" + $"{a2[0]} du ?");
                                        //Parser.Pars(a2[0] + " = " + a2[1], file, func, poz, peremen, typeperemen, macroses);
                                    }
                                    else
                                    { //File.AppendAllText(file, "\n" + $"{a2[0]} du {a2[1].Trim()}");
                                    }
                                }
                            }

                        }
                        else
                        {
                            string[] a2 = parts[1].Split(new char[] { '=' }, 2).Select(s => s.Trim()).ToArray();
                            peremen.Add(a2[0].Trim());
                            typeperemen.Add("dword");
                            //File.AppendAllText(file, "\n" + $"{command.TrimStart().Substring(6)} du ?");
                        }
                    }
                    else if (command.TrimStart().StartsWith("qword"))
                    {
                        string[] parts = command.TrimStart().Split(new char[] { ' ' }, 2);
                        if (parts.Length > 1 && parts[1].Contains("="))
                        {
                            string[] a2 = parts[1].Split(new char[] { '=' }, 2).Select(s => s.Trim()).ToArray();
                            if (a2.Length > 1)
                            {
                                if (a2[1] == "null")
                                { //File.AppendAllText(file, "\n" + $"{a2[0]} dq ?");
                                }
                                else
                                {
                                    peremen.Add(a2[0].Trim());
                                    typeperemen.Add("qword");
                                    if (!char.IsNumber(a2[1].Trim()[0]) && !a2[1].Trim().StartsWith("\"") && !a2[1].Trim().Replace(" ", null).StartsWith("'") && !instruct)
                                    {
                                        //File.AppendAllText(file, "\n" + $"{a2[0]} dq ?");
                                        //Parser.Pars(a2[0] + " = " + a2[1], file, func, poz, peremen, typeperemen, macroses);
                                    }
                                    else
                                    { //File.AppendAllText(file, "\n" + $"{a2[0]} dq {a2[1].Trim()}");
                                    }
                                }
                            }

                        }
                        else
                        {
                            string[] a2 = parts[1].Split(new char[] { '=' }, 2).Select(s => s.Trim()).ToArray();
                            peremen.Add(a2[0].Trim());
                            typeperemen.Add("dword");
                            //File.AppendAllText(file, "\n" + $"{command.TrimStart().Substring(6)} dd ?");
                        }
                    }
                }
            }
            catch { Console.WriteLine("Error: Error in variable syntax! On line: " + (poz + 1)); error++; }
        }
    }
}
