namespace Lumin
{
    static unsafe public class Parser
    {

        private static bool ProcessCommand(string command, int index)
        {

            bool inSingleQuotes = false;
            bool inDoubleQuotes = false;

            //  Console.WriteLine(index);



            for (int i = 0; i < index; i++)
            {
                char currentChar = command[i];
                if (currentChar == '\'')
                {
                    inSingleQuotes = true;
                }
                else if (currentChar == '\"')
                {
                    inDoubleQuotes = true;
                }
            }

            // Если мы нашли оператор вне кавычек
            if (!inSingleQuotes && !inDoubleQuotes)
            {
                return true;
            }
            return false;
        }




        static bool HasS(string input)
        {
            bool inSingleQuote = false;
            bool inDoubleQuote = false;

            for (int i = 0; i < input.Length; i++)
            {
                char c = input[i];


                if (c == '"')
                {
                    if (!inSingleQuote)
                    {
                        inDoubleQuote = !inDoubleQuote;
                    }
                }

                else if (c == '\'')
                {
                    if (!inDoubleQuote)
                    {
                        inSingleQuote = !inSingleQuote;
                    }
                }

                else if (!inSingleQuote && !inDoubleQuote)
                {

                    if (c == '*' ||
                        (c == '+' && i < input.Length - 1 && input[i + 1] == '=') ||
                        (c == '=' && i > 0 && input[i - 1] == '+') ||
                        (c == '-' && i < input.Length - 1 && input[i + 1] == '=') ||
                        (c == '/'))
                    {
                        return true;
                    }
                }
            }

            return false;
        }
        public static int isperemen(string pere, List<string> peremen, List<string> typeperemen)
        {
            for (int i = 0; i < peremen.Count(); i++)
            {
                if (pere.Replace("@", null) == peremen[i])
                {
                    return i;
                }
                if (pere.Contains("."))
                {
                    string[] p = pere.TrimStart().Split('.', 2);
                    if (p[1].Replace("@", null) == peremen[i])
                    {
                        return i;
                    }
                }
                if (pere.Contains(","))
                {
                    string[] p = pere.TrimStart().Split(',', 2);
                   // Console.WriteLine(p[0]);
                   // Console.WriteLine(peremen[i]);
                    if (p[0].Replace("@", null) == peremen[i])
                    {
                        return i;
                    }
                }

            }
            return -1;
        }
        static List<string> registers = new List<string>
        {
            "EAX",   // Accumulator register
            "EBX",   // Base register
            "ECX",   // Counter register
            "EDX",   // Data register
            "ESI",   // Source Index
            "EDI",   // Destination Index
            "ESP",   // Stack Pointer
            "EBP",   // Base Pointer
            "EIP",   // Instruction Pointer
            "AX",    // 16-bit Accumulator
            "BX",    // 16-bit Base
            "CX",    // 16-bit Counter
            "DX",    // 16-bit Data
            "SI",    // 16-bit Source Index
            "DI",    // 16-bit Destination Index
            "SP",    // 16-bit Stack Pointer
            "BP",    // 16-bit Base Pointer
            "RAX",   // 64-bit Accumulator
            "RBX",   // 64-bit Base
            "RCX",   // 64-bit Counter
            "RDX",   // 64-bit Data
            "RSI",   // 64-bit Source Index
            "RDI",   // 64-bit Destination Index
            "RSP",   // 64-bit Stack Pointer
            "RBP",   // 64-bit Base Pointer
            "RIP"    // 64-bit Instruction Pointer
        };
        static public bool Pars(string command, string file, List<string> func, int poz, List<string> peremen, List<string> typeperemen, List<string> macroses, List<string> dllfunc,List<string> stringstoaddend,int strlasted)
        {
            bool tes = false;
            string pattern = @"\d+";
            //z  if (HasS(command))
            // {
            //  Console.WriteLine(command);

            try
            {
                int index5 = command.IndexOf("=");
                if (ProcessCommand(command, index5))
                {
                    if (command.Contains("=") && command[index5 - 1] != '+' && command[index5 - 1] != '-' && command[index5 + 1] != '@')
                    {

                        string[] a = command.TrimStart().Split("=", 2);
                        //bool containsNumbers = Regex.IsMatch(a[1], pattern);
                        int isp = isperemen(a[0].Trim(), peremen, typeperemen);
                        if (a[1].TrimEnd().EndsWith("@") && a[1].TrimEnd().Contains("\""))
                        {
                           //Console.WriteLine(a[1].TrimStart().Substring(0, a[1].Length - 1));
                            a[1] = helpfunc.parsstring(a[1].TrimStart().TrimEnd().Substring(0, a[1].Length-2), true,stringstoaddend,strlasted);
                        }
                        if (!Sas.Parscall(file, a[1].TrimStart(), func, poz, peremen, typeperemen, macroses, dllfunc, true, false))
                        {
                            int isp2 = isperemen(a[1].Trim(), peremen, typeperemen);
                            if (char.IsLetter(a[1].Trim()[0]) && a[1].TrimStart().Contains("'") == false)
                            {
                                if (isp2 == -1)
                                {
                                    File.AppendAllText(file, "\n" + $"mov [{a[0]}],[{a[1]}]"); return true;
                                }
                            }
                            else
                            {
                                if (isp2 == -1)
                                {
                                    File.AppendAllText(file, "\n" + $"mov [{a[0]}],{a[1]}"); return true;
                                }
                            }
                           
                            if (!helpfunc.ismacro(a[1], func, file, a[1], a[0]))
                            {
                                if (char.IsLetter(a[1].Trim()[0]) && a[1].TrimStart().Contains("'") == false)
                                {
                                    if (isp == -1)
                                    {
                                        File.AppendAllText(file, "\n" + $"mov [{a[0]}],[{a[1]}]");
                                    }
                                    if (typeperemen[isp] == "dword")
                                    {
                                        File.AppendAllText(file, "\n" + $"\nmov eax,[{a[1]}]\nmov [{a[0]}],eax");
                                    }
                                    else if (typeperemen[isp] == "byte")
                                    {
                                        File.AppendAllText(file, "\n" + $"\nmov bl,[{a[1]}]\nmov [{a[0]}],bl");
                                    }
                                    else if (typeperemen[isp] == "word" || typeperemen[isp] == "ubyte")
                                    {
                                        File.AppendAllText(file, "\n" + $"\nmov ax,[{a[1]}]\nmov [{a[0]}],ax");
                                    }
                                    else if (typeperemen[isp] == "tword" || typeperemen[isp] == "qword")
                                    {
                                        File.AppendAllText(file, "\n" + $"\nmov rax,[{a[1]}]\nmov [{a[0]}],rax");
                                    }

                                }
                                else
                                {
                                    
                                    if (isp == -1)
                                    {
                                        File.AppendAllText(file, "\n" + $"mov [{a[0]}],[{a[1]}]");
                                    }
                                    if (typeperemen[isp] == "dword")
                                    {
                                        File.AppendAllText(file, "\n" + $"\nmov eax,{a[1]}\nmov [{a[0]}],eax");
                                    }
                                    else if (typeperemen[isp] == "byte")
                                    {
                                        File.AppendAllText(file, "\n" + $"\nmov bl,{a[1]}\nmov [{a[0]}],bl");
                                    }
                                    else if (typeperemen[isp] == "word" || typeperemen[isp] == "ubyte")
                                    {
                                        File.AppendAllText(file, "\n" + $"\nmov ax,{a[1]}\nmov [{a[0]}],ax");
                                    }
                                    else if (typeperemen[isp] == "tword" || typeperemen[isp] == "qword")
                                    {
                                        File.AppendAllText(file, "\n" + $"\nmov rax,{a[1]}\nmov [{a[0]}],rax");
                                    }
                                    else
                                    {
                                        File.AppendAllText(file, "\n" + $"mov [{a[0]}],[{a[1]}]");
                                    }
                                }
                            }
                        }
                        else
                        {

                            File.AppendAllText(file, $"\nmov [{a[0]}],eax");
                        }
                        return true;


                    }
                }
                int index0 = command.IndexOf("=@");
                if (ProcessCommand(command, index0))
                {
                    if (command.Contains("=@"))
                    {


                        string[] a = command.TrimStart().Split("=@", 2);

                        int isp = isperemen(a[0].Trim().Replace(" ", null), peremen, typeperemen);
                        int isp2 = isperemen(a[1].Trim(), peremen, typeperemen);
                        if (char.IsLetter(a[1].Trim()[0]) && a[1].TrimStart().Contains("'") == false)
                        {
                            if (isp2 == -1)
                            {
                                File.AppendAllText(file, "\n" + $"lea [{a[0]}],[{a[1]}]"); return true;
                            }
                        }
                        else
                        {
                            if (isp2 == -1)
                            {
                                File.AppendAllText(file, "\n" + $"lea [{a[0]}],{a[1]}"); return true;
                            }
                        }
                        if (!helpfunc.ismacro(a[1], func, file, a[1], a[0]))
                        {

                            if (char.IsLetter(a[1].Trim()[0]) && a[1].TrimStart().Contains("'") == false)
                            {
                                if (typeperemen[isp] == "dword")
                                {
                                    File.AppendAllText(file, "\n" + $"\nmov eax,[{a[1]}]\nlea [{a[0]}],eax");
                                }
                                else if (typeperemen[isp] == "byte")
                                {
                                    File.AppendAllText(file, "\n" + $"\nmov bl,[{a[1]}]\nlea [{a[0]}],bl");
                                }
                                else if (typeperemen[isp] == "word" || typeperemen[isp] == "ubyte")
                                {
                                    File.AppendAllText(file, "\n" + $"\nmov ax,[{a[1]}]\nlea [{a[0]}],ax");
                                }
                                else if (typeperemen[isp] == "tword" || typeperemen[isp] == "qword")
                                {
                                    File.AppendAllText(file, "\n" + $"\nmov rax,[{a[1]}]\nlea [{a[0]}],rax");
                                }

                            }
                            else
                            {
                                if (typeperemen[isp] == "dword")
                                {
                                    File.AppendAllText(file, "\n" + $"\nmov eax,{a[1]}\nlea [{a[0]}],eax");
                                }
                                else if (typeperemen[isp] == "byte")
                                {
                                    File.AppendAllText(file, "\n" + $"\nmov bl,{a[1]}\nlea [{a[0]}],bl");
                                }
                                else if (typeperemen[isp] == "word" || typeperemen[isp] == "ubyte")
                                {
                                    File.AppendAllText(file, "\n" + $"\nmov ax,{a[1]}\nlea [{a[0]}],ax");
                                }
                                else if (typeperemen[isp] == "tword" || typeperemen[isp] == "qword")
                                {
                                    File.AppendAllText(file, "\n" + $"\nmov rax,{a[1]}\nlea [{a[0]}],rax");
                                }
                            }
                            return true;
                        }
                    }
                }
                int index = command.IndexOf("+=");
                if (ProcessCommand(command, index))
                {
                    if (command.Contains("+="))
                    {


                        string[] a = command.TrimStart().Split("+=", 2);
                        //bool containsNumbers = Regex.IsMatch(a[1], pattern);
                        int isp = isperemen(a[0].Trim(), peremen, typeperemen);
                        if (!Sas.Parscall(file, a[1].TrimStart(), func, poz, peremen, typeperemen, macroses, dllfunc, true, false))
                        {
                            int isp2 = isperemen(a[1].Trim(), peremen, typeperemen);
                            if (char.IsLetter(a[1].Trim()[0]) && a[1].TrimStart().Contains("'") == false)
                            {
                                if (isp2 == -1)
                                {
                                    File.AppendAllText(file, "\n" + $"add [{a[0]}],[{a[1]}]"); return true;
                                }
                            }
                            else
                            {
                                if (isp2 == -1)
                                {
                                    File.AppendAllText(file, "\n" + $"add [{a[0]}],{a[1]}"); return true;
                                }
                            }
                            if (!helpfunc.ismacro(a[1], func, file, a[1], a[0]))
                            {
                                if (char.IsLetter(a[1].Trim()[0]) && a[1].TrimStart().Contains("'") == false)
                                {

                                    if (typeperemen[isp] == "dword")
                                    {
                                        File.AppendAllText(file, "\n" + $"\nmov eax,[{a[1]}]\nadd [{a[0]}],eax");
                                    }
                                    else if (typeperemen[isp] == "byte")
                                    {
                                        File.AppendAllText(file, "\n" + $"\nmov bl,[{a[1]}]\nadd [{a[0]}],bl");
                                    }
                                    else if (typeperemen[isp] == "word" || typeperemen[isp] == "ubyte")
                                    {
                                        File.AppendAllText(file, "\n" + $"\nmov ax,[{a[1]}]\nadd [{a[0]}],ax");
                                    }
                                    else if (typeperemen[isp] == "tword" || typeperemen[isp] == "qword")
                                    {
                                        File.AppendAllText(file, "\n" + $"\nmov rax,[{a[1]}]\nadd [{a[0]}],rax");
                                    }
                                }
                                else
                                {


                                    if (typeperemen[isp] == "dword")
                                    {
                                        File.AppendAllText(file, "\n" + $"\nmov eax,{a[1]}\nadd [{a[0]}],eax");
                                    }
                                    else if (typeperemen[isp] == "byte")
                                    {
                                        File.AppendAllText(file, "\n" + $"\nmov bl,{a[1]}\nadd [{a[0]}],bl");
                                    }
                                    else if (typeperemen[isp] == "word" || typeperemen[isp] == "ubyte")
                                    {
                                        File.AppendAllText(file, "\n" + $"\nmov ax,{a[1]}\nadd [{a[0]}],ax");
                                    }
                                    else if (typeperemen[isp] == "tword" || typeperemen[isp] == "qword")
                                    {
                                        File.AppendAllText(file, "\n" + $"\nmov rax,{a[1]}\nadd [{a[0]}],rax");
                                    }
                                }
                            }
                        }
                        else
                        {

                            File.AppendAllText(file, $"\nadd [{a[0]}],eax");
                        }

                        return true;

                    }
                }
                int index2 = command.IndexOf("-=");
                if (ProcessCommand(command, index2))
                {
                    if (command.Contains("-="))
                    {


                        string[] a = command.TrimStart().Split("-=", 2);
                        //bool containsNumbers = Regex.IsMatch(a[1], pattern);
                        int isp = isperemen(a[0].Trim(), peremen, typeperemen);
                        if (!Sas.Parscall(file, a[1].TrimStart(), func, poz, peremen, typeperemen, macroses, dllfunc, true, false))
                        {
                            int isp2 = isperemen(a[1].Trim(), peremen, typeperemen);
                            if (char.IsLetter(a[1].Trim()[0]) && a[1].TrimStart().Contains("'") == false)
                            {
                                if (isp2 == -1)
                                {
                                    File.AppendAllText(file, "\n" + $"sub [{a[0]}],[{a[1]}]"); return true;
                                }
                            }
                            else
                            {
                                if (isp2 == -1)
                                {
                                    File.AppendAllText(file, "\n" + $"sub [{a[0]}],{a[1]}"); return true;
                                }
                            }
                            if (!helpfunc.ismacro(a[1], func, file, a[1], a[0]))
                            {
                                if (char.IsLetter(a[1].Trim()[0]) && a[1].TrimStart().Contains("'") == false)
                                {

                                    if (typeperemen[isp] == "dword")
                                    {
                                        File.AppendAllText(file, "\n" + $"\nmov eax,[{a[1]}]\nsub [{a[0]}],eax");
                                    }
                                    else if (typeperemen[isp] == "byte")
                                    {
                                        File.AppendAllText(file, "\n" + $"\nmov bl,[{a[1]}]\nsub [{a[0]}],bl");
                                    }
                                    else if (typeperemen[isp] == "word" || typeperemen[isp] == "ubyte")
                                    {
                                        File.AppendAllText(file, "\n" + $"\nmov ax,[{a[1]}]\nsub [{a[0]}],ax");
                                    }
                                    else if (typeperemen[isp] == "tword" || typeperemen[isp] == "qword")
                                    {
                                        File.AppendAllText(file, "\n" + $"\nmov rax,[{a[1]}]\nsub [{a[0]}],rax");
                                    }
                                }
                                else
                                {


                                    if (typeperemen[isp] == "dword")
                                    {
                                        File.AppendAllText(file, "\n" + $"\nmov eax,{a[1]}\nsub [{a[0]}],eax");
                                    }
                                    else if (typeperemen[isp] == "byte")
                                    {
                                        File.AppendAllText(file, "\n" + $"\nmov bl,{a[1]}\nsub [{a[0]}],bl");
                                    }
                                    else if (typeperemen[isp] == "word" || typeperemen[isp] == "ubyte")
                                    {
                                        File.AppendAllText(file, "\n" + $"\nmov ax,{a[1]}\nsub [{a[0]}],ax");
                                    }
                                    else if (typeperemen[isp] == "tword" || typeperemen[isp] == "qword")
                                    {
                                        File.AppendAllText(file, "\n" + $"\nmov rax,{a[1]}\nsub [{a[0]}],rax");
                                    }
                                }
                            }
                            else
                            {

                                File.AppendAllText(file, $"\nsub [{a[0]}],eax");
                            }
                            return true;
                        }

                    }
                }
                int index3 = command.IndexOf("*");
                if (ProcessCommand(command, index))
                {
                    if (command.Contains('*') && index3 < command.IndexOf('/') || command.IndexOf('/') == -1 && command.Contains('*'))
                    {

                        string[] a = command.TrimStart().Split("*", 2);
                        //bool containsNumbers = Regex.IsMatch(a[1], pattern);
                        int isp = isperemen(a[0].Trim(), peremen, typeperemen);
                        if (!Sas.Parscall(file, a[1].TrimStart(), func, poz, peremen, typeperemen, macroses, dllfunc, true, false))
                        {
                            int isp2 = isperemen(a[1].Trim(), peremen, typeperemen);
                            if (char.IsLetter(a[1].Trim()[0]) && a[1].TrimStart().Contains("'") == false)
                            {
                                if (isp2 == -1)
                                {
                                    File.AppendAllText(file, "\n" + $"imul [{a[0]}],[{a[1]}]"); return true;
                                }
                            }
                            else
                            {
                                if (isp2 == -1)
                                {
                                    File.AppendAllText(file, "\n" + $"imul [{a[0]}],{a[1]}"); return true;
                                }
                            }
                            if (!helpfunc.ismacro(a[1], func, file, a[1], a[0]))
                            {
                                if (char.IsLetter(a[1].Trim()[0]) && a[1].TrimStart().Contains("'") == false)
                                {

                                    if (typeperemen[isp] == "dword")
                                    {
                                        File.AppendAllText(file, "\n" + $"\nmov eax,[{a[1]}]\nimul eax,[{a[0]}]\nmov[{a[0]}],eax");
                                    }
                                    else if (typeperemen[isp] == "byte")
                                    {
                                        File.AppendAllText(file, "\n" + $"\nmov bl,[{a[1]}]\nimul bl,[{a[0]}]\nmov[{a[0]}],bl");
                                    }
                                    else if (typeperemen[isp] == "word" || typeperemen[isp] == "ubyte")
                                    {
                                        File.AppendAllText(file, "\n" + $"\nmov ax,[{a[1]}]\nimul ax,[{a[0]}]\nmov[{a[0]}],ax");
                                    }
                                    else if (typeperemen[isp] == "tword" || typeperemen[isp] == "qword")
                                    {
                                        File.AppendAllText(file, "\n" + $"\nmov rax,[{a[1]}]\nimul rax,[{a[0]}]\nmov[{a[0]}],rax");
                                    }
                                }
                                else
                                {


                                    if (typeperemen[isp] == "dword")
                                    {
                                        File.AppendAllText(file, "\n" + $"\nmov eax,{a[1]}\nimul eax,{a[0]}\nmov {a[0]},eax");
                                    }
                                    else if (typeperemen[isp] == "byte")
                                    {
                                        File.AppendAllText(file, "\n" + $"\nmov bl,{a[1]}\nimul bl,{a[0]}\nmov {a[0]},eax");
                                    }
                                    else if (typeperemen[isp] == "word" || typeperemen[isp] == "ubyte")
                                    {
                                        File.AppendAllText(file, "\n" + $"\nmov ax,{a[1]}\nimul ax,{a[0]}\nmov[{a[0]}],ax");
                                    }
                                    else if (typeperemen[isp] == "tword" || typeperemen[isp] == "qword")
                                    {
                                        File.AppendAllText(file, "\n" + $"\nmov rax,{a[1]}\nimul rax,{a[0]}\nmov[{a[0]}],rax");
                                    }
                                }
                            }
                            else
                            {

                                File.AppendAllText(file, $"\nimul [{a[0]}],eax");
                            }
                            return true;
                        }
                    }
                }
                int index4 = command.IndexOf("/");
                if (ProcessCommand(command, index))
                {

                    if (command.Contains('/') && index4 < command.IndexOf('*') || command.IndexOf('*') == -1 && command.Contains('/'))
                    {
                        string[] a = command.TrimStart().Split("/", 2);
                        int isp = isperemen(a[0].Trim(), peremen, typeperemen);
                        if (!Sas.Parscall(file, a[1].TrimStart(), func, poz, peremen, typeperemen, macroses, dllfunc, true, false))
                        {
                            int isp2 = isperemen(a[1].Trim(), peremen, typeperemen);
                        if (char.IsLetter(a[1].Trim()[0]) && a[1].TrimStart().Contains("'") == false)
                        {
                            if (isp2 == -1)
                            {
                                File.AppendAllText(file, "\n" + $"idiv [{a[0]}],[{a[1]}]"); return true;
                            }
                        }
                        else
                        {
                            if (isp2 == -1)
                            {
                                File.AppendAllText(file, "\n" + $"idiv [{a[0]}],{a[1]}"); return true;
                            }
                        }
                        if (!helpfunc.ismacro(a[1], func, file, a[1], a[0]))
                        {
                            string dividendString = a[1].Trim();
                            string divisorString = a[0].Trim();

                            if (char.IsLetter(dividendString[0]) && !dividendString.Contains("'"))
                            {
                                if (typeperemen[isp] == "dword")
                                {
                                    File.AppendAllText(file, $"\nmov eax, [{dividendString}]\nmov ebx, [{divisorString}]\n" +
                                                           "xor edx, edx\n" +
                                                           "idiv ebx\n" +
                                                           $"mov [{divisorString}], eax\n");
                                }
                                else if (typeperemen[isp] == "byte")
                                {
                                    File.AppendAllText(file, $"\nmov al, [{dividendString}]\nmov bl, [{divisorString}]\n" +
                                                           "xor ah, ah\n" +
                                                           "idiv bl\n" +
                                                           $"mov [{divisorString}], al\n");
                                }
                                else if (typeperemen[isp] == "word" || typeperemen[isp] == "ubyte")
                                {
                                    File.AppendAllText(file, $"\nmov ax, [{dividendString}]\nmov bx, [{divisorString}]\n" +
                                                           "xor dx, dx\n" +
                                                           "idiv bx\n" +
                                                           $"mov [{divisorString}], ax\n");
                                }
                                else if (typeperemen[isp] == "tword" || typeperemen[isp] == "qword")
                                {
                                    File.AppendAllText(file, $"\nmov rax, [{dividendString}]\nmov rbx, [{divisorString}]\n" +
                                                           "xor rdx, rdx\n" +
                                                           "idiv rbx\n" +
                                                           $"mov [{divisorString}], rax\n");
                                }
                            }
                            else
                            {
                                if (typeperemen[isp] == "dword")
                                {
                                    File.AppendAllText(file, $"\nmov eax, {dividendString}\nmov ebx, [{divisorString}]\n" +
                                                           "xor edx, edx\n" +
                                                           "idiv ebx\n" +
                                                           $"mov [{divisorString}], eax\n");
                                }
                                else if (typeperemen[isp] == "byte")
                                {
                                    File.AppendAllText(file, $"\nmov al, {dividendString}\nmov bl, [{divisorString}]\n" +
                                                           "xor ah, ah\n" +
                                                           "idiv bl\n" +
                                                           $"mov [{divisorString}], al\n");
                                }
                                else if (typeperemen[isp] == "word" || typeperemen[isp] == "ubyte")
                                {
                                    File.AppendAllText(file, $"\nmov ax, {dividendString}\nmov bx, [{divisorString}]\n" +
                                                           "xor dx, dx\n" +
                                                           "idiv bx\n" +
                                                           $"mov [{divisorString}], ax\n");
                                }
                                else if (typeperemen[isp] == "tword" || typeperemen[isp] == "qword")
                                {
                                    File.AppendAllText(file, $"\nmov rax, {dividendString}\nmov rbx, [{divisorString}]\n" +
                                                           "xor rdx, rdx\n" +
                                                           "idiv rbx\n" +
                                                           $"mov [{divisorString}], rax\n");
                                }
                            }
                            }
                            else
                            {

                                File.AppendAllText(file, $"mov ebx, [{a[0]}]\n" +
                                                          "xor edx, edx\n" +
                                                          "idiv ebx\n" +
                                                          $"mov [{a[0]}], eax\n");
                            }
                            return true;
                        }
                    }
                }
                int index7 = command.IndexOf("<<");
                if (ProcessCommand(command, index7))
                {
                    if (command.Contains("<<"))
                    {
                        string[] a = command.TrimStart().Split("<<");
                        File.AppendAllText(file, $"\nshl [{a[0]}],[{a[1]}]");
                        return true;
                    }
                }
                int index8 = command.IndexOf(">>");
                if (ProcessCommand(command, index8))
                {
                    if (command.Contains(">>"))
                    {
                        string[] a = command.TrimStart().Split(">>");
                        File.AppendAllText(file, $"\nshr [{a[0]}],[{a[1]}]");
                        return true;
                    }
                }
                int index9 = command.IndexOf("++");
                if (ProcessCommand(command, index9))
                {
                    if (command.Contains("++"))
                    {

                        File.AppendAllText(file, "\ninc [" + command.Replace("++", null).Trim() + "]");
                        return true;
                    }
                }
                int index10 = command.IndexOf("--");
                if (ProcessCommand(command, index10))
                {
                    if (command.Contains("--"))
                    {

                        File.AppendAllText(file, "\ndec [" + command.Replace("--", null).Trim() + "]");
                        return true;
                    }
                }
                return default;
            }

            catch (Exception)
            {
                Console.WriteLine("Error: Error in actions with variables! On line: " + (poz + 1));

            }
            return default;
            //}
            //else { return false; }
        }
    }
}
