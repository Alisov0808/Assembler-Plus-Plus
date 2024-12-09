using Lumin;
using System.Diagnostics;
using System.Runtime;
using System.Text;
using System.Text.RegularExpressions;
public unsafe class Sas
{
    static string st = "";
    static List<string> stringstoaddend = new List<string>();
    static int stringlasted = 0;
    static public Dictionary<string, string> registers = new Dictionary<string, string>
        {
            { "eax", "dword" },
            { "ebx", "dword" },
            { "ecx", "dword" },
            { "edx", "dword" },
            { "esi", "dword" },
            { "edi", "dword" },
            { "ebp", "dword" },
            { "esp", "dword" },
            { "eip", "dword" },

            // 8-битные регистры
            { "al", "byte" },
            { "ah", "byte" },
            { "bl", "byte" },
            { "bh", "byte" },
            { "cl", "byte" },
            { "ch", "byte" },
            { "dl", "byte" },
            { "dh", "byte" },

            // 16-битные регистры
            { "ax", "word" },
            { "bx", "word" },
            { "cx", "word" },
            { "dx", "word" },
            { "si", "word" },
            { "di", "word" },
            { "bp", "word" },
            { "sp", "word" },

            // 64-битные регистры (на архитектуре x86-64)
            { "rax", "qword" },
            { "rbx", "qword" },
            { "rcx", "qword" },
            { "rdx", "qword" },
            { "rsi", "qword" },
            { "rdi", "qword" },
            { "rbp", "qword" },
            { "rsp", "qword" },
            { "rip", "qword" },
            { "r8",  "qword" },
            { "r9",  "qword" },
            { "r10", "qword" },
            { "r11", "qword" },
            { "r12", "qword" },
            { "r13", "qword" },
            { "r14", "qword" },
            { "r15", "qword" }
        };


    public string Interpret(string command)
    {
        return command;
        Console.WriteLine("Intepreting finished!");
    }
  

    static void Main(string[] args)
    {

        if (args.Length > 0)
        {
            Main2(args[0].ToLower());
        }
        else
        {
            Main2();
        }
    }
    static void Main2(string args = null, string close8 = "")
    {

        bool close2 = false;
        if (close8 == "y")
        {
            close2 = true;
        }
        string className = "";
        bool inClass = false;
        // Для хранения обработанного класса
        int lastMatchIndex = 0;
        string text2 = null;
        bool boot = false;
        bool linconsole = false;
        bool console = false;
        bool nonos = false;
        int poz = 0;
        bool ferifyboot = false;
        string inputFilePath = "";
        int errorcount = 0;
        if (args == null)
        {
            inputFilePath = consoleman.Parscon();

        }
        else
        {
            inputFilePath = args;
        }
        if (!File.Exists(inputFilePath)) { Console.WriteLine("Error: File not found!"); errorcount++; Thread.Sleep(5000); }
        Console.WriteLine("Интерпретация...");
        List<string> commands = File.ReadAllLines(inputFilePath).ToList();
        string[] reserv = commands.ToArray();
        string file = inputFilePath.ToLower().Replace(".lum", ".asm");
        int mat = 0;
        bool was = false;
        int last = 0;
        int realpoz = 0;


        List<string> func = new List<string>();
        List<string> struc = new List<string>();
        List<string> peremen = new List<string>();
        List<string> typeperemen = new List<string>();
        List<string> macroses = new List<string>();
        List<string> dllfunc = new List<string>();

        try
        {


           File.WriteAllText(file, "");
        }
        catch (Exception)
        {

            Console.WriteLine();
        }
        Stopwatch stopwatch = new Stopwatch();
        //   List<string> lines = new List<string>(commands.Split(new[] { Environment.NewLine }, StringSplitOptions.None));
        List<string> outputLines = new List<string>();
        List<string> classOutput = new List<string>();
        List<int> pointof = new List<int>();
        List<string> classn = new List<string>();
        List<string> whatin = new List<string>();

        //foreach (var reg in registers)
        //{
        //    peremen.Add(reg.Key);
        //    typeperemen.Add(reg.Value);
        //}

        stopwatch.Start();
        string cls = "";
        bool instrict = false;
        int counted = 0;
        bool chk = false;
        bool wingui = false;
        bool inStr = false; int braceCount = 0;
        int laststack = 0;

        other.Pars(commands, inputFilePath, func, poz, file, struc, inClass, cls, outputLines, className, inStr, macroses, whatin, classn, braceCount, pointof, peremen, typeperemen, errorcount, instrict, laststack);

        bool inforeach = false;
        bool wasassemb = false;
        bool infor = false;
        bool inwhile = false;
        bool inif = false;
        bool indowhile = false;
        int lastwhile = 0;
        string forwat = "";
        string watconwhile = "";
        List<string> preorityloop = new List<string>();
        List<int> preorityloopindex = new List<int>();
        List<string> watconwhilr = new List<string>();

        string watconwhiledo = "";
        int lastwhiledo = 999;
        int lastif = 4546;
        long lastfor = 6765;
        List<string> confor = new List<string>();
        string forcmp = "";
        List<string> watcondo = new List<string>();

        List<string> todelete = new List<string>();
        string foreachstring = "";
        List<string> lastnamefor = new List<string>(); List<string> lastnameforeach = new List<string>(); List<string> lastnamewhile = new List<string>(); List<string> lastnamedowhile = new List<string>();
        string importhead = "";
        string importside = "";
        int brasecouif = 0;
        int brasecouwhile = 0;
        int brasecoudowhile = 0;
        int brasecoufor = 0;
        int brasecouforeach = 0;
        string mode = "";
        bool wasbrk = false;
        string realcommandto = "";



        foreach (string command in outputLines)
        {
           realcommandto = command;

            poz++;
            realpoz++;
            if (!wasassemb)
            {
                //else if (realcommandto.TrimStart().StartsWith("if") || realcommandto.TrimStart().StartsWith("else") || realcommandto.TrimStart().StartsWith("compare"))
                //{
                //    If_and_loop g = new If_and_loop();
                //    g.ParsIf(realcommandto, poz, file, inputFilePath, func, errorcount);

                //}
                if (realcommandto.TrimStart().StartsWith("undefine "))
                {
                    for (int u = 0; u < other.define.Count(); u++)
                    {
                        Console.WriteLine(other.define[u] + " f");
                        if (other.define[u] == realcommandto.Substring(9).Trim())
                        {

                            other.define.RemoveAt(u);
                            other.watdefine.RemoveAt(u);

                        }
                    }
                }
                for (int u = 0; u < other.define.Count(); u++)
                {
                    // Console.WriteLine(define[u] + " f");
                    string pattern = @"(?<!['""])\b" + Regex.Escape(other.define[u]) + @"\b(?!['""])";

                    realcommandto = Regex.Replace(realcommandto, pattern, other.watdefine[u]);

                }

                if (realcommandto.TrimStart().StartsWith("label "))
                {
                    string command2 = realcommandto;
                    string isp = null;
                    if (command2.Contains("{")) { command2 = command2.Replace("{", ""); }
                    string[] con = command2.Replace("label", null).Split(":", 2);
                    File.AppendAllText(file, "\n" + con[0] + ":");
                    realcommandto = con[1];
                    int currentPosition = poz;
                    string fileContent = File.ReadAllText(file);
                    string[] lines2 = fileContent.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
                    File.WriteAllLines(file, lines2);

                    func.Add(con[0].Replace(" ", "").Trim().Replace("label", null));



                }
                else if (realcommandto.Trim().Replace(" ", null) == "break")
                {
                    wasbrk = true;
                    File.AppendAllText(file, "\njmp .breakloopLC07" + realpoz);
                    if (preorityloop[preorityloop.Count() - 1] == "foreach")
                    {
                        lastnameforeach.Add("\n.breakloopLC07" + realpoz + ":");

                    }
                    else if (preorityloop[preorityloop.Count() - 1] == "for")
                    {
                        lastnamefor.Add("\n.breakloopLC07" + realpoz + ":");
                    }
                    else if (preorityloop[preorityloop.Count() - 1] == "while")
                    {
                        lastnamewhile.Add("\n.breakloopLC07" + realpoz + ":");
                    }
                    else if (preorityloop[preorityloop.Count() - 1] == "dowhile")
                    {
                        lastnamedowhile.Add("\n.breakloopLC07" + realpoz + ":");
                    }
                    continue;
                }
                else if (realcommandto.TrimStart().StartsWith("while "))
                {
                    lastwhile++;
                    If_and_loop.ParsIf(realcommandto.TrimStart(), poz, file, inputFilePath, func, errorcount, true);
                    brasecouwhile++;
                    //File.AppendAllText(file, $"\n.loopLC00h7{realpoz}:\n");
                    //If_and_loop.Parswhile(realcommandto, poz, file, inputFilePath, func, errorcount);
                    //List<string> lines = File.ReadAllLines(file).ToList();
                    //for (int i = 0; i < lines.Count(); i++)
                    //{
                    //    if (lines[i] == $".loopLC00h7{realpoz}:")
                    //    {
                    //        lines[i] = "";

                    //    }
                    //    if (lines[i].TrimStart().Contains("j") && lines[i].TrimStart().Contains($".loopLC00h7{realpoz}"))
                    //    {

                    //        string[] p = lines[i].TrimStart().Split(' ', 2);
                    //        watconwhilr.Add(p[0]);
                    //    }
                    //}
                    //watconwhile = lines[lines.Count - 2];
                    preorityloop.Add("while");
                    //File.WriteAllLines(file, lines);
                    //File.AppendAllText(file, $"\njmp .whileLC025{lastwhile}\n.loopLC00h7{realpoz}:\n");
                    inwhile = true; continue;
                }
                else if (realcommandto.TrimStart().StartsWith("dowhile "))
                {
                    lastwhile++;
                    File.AppendAllText(file, "\n.dowhile");
                    brasecoudowhile++;
                    string[] a = realcommandto.TrimStart().Split(' ', 2);
                    watcondo.Add(a[1]);
                    preorityloop.Add("dowhile");

                    indowhile = true; continue;
                }
                else if (realcommandto.TrimStart().StartsWith("if "))
                {
                    preorityloop.Add("if");

                    lastif++;
                    brasecouif++;
                    preorityloopindex.Add(lastif);
                    If_and_loop.ParsIf(realcommandto.TrimStart(), poz, file, inputFilePath, func, errorcount, false);
                    //File.AppendAllText(file, $"\n.looopLC00h7{realpoz}:\n");
                    //If_and_loop.Parswhile("while " + realcommandto.TrimStart().Substring(3), poz, file, inputFilePath, func, errorcount);
                    //List<string> lines = File.ReadAllLines(file).ToList();
                    //for (int i = lines.Count() - 1; i > 0; i--)
                    //{
                    //    if (lines[i] == $".looopLC00h7{realpoz}:")
                    //    {
                    //        lines[i] = "";
                    //        break;
                    //    }

                    //}

                    //File.WriteAllLines(file, lines);
                    //File.AppendAllText(file, $"\njmp .whileLC025{lastif}\n.looopLC00h7{realpoz}:\n");
                    inif = true; continue;
                }
                else if (realcommandto.TrimStart().StartsWith("else"))
                {
                    preorityloop.Add("if");

                    lastif++;
                    brasecouif++;
                    preorityloopindex.Add(lastif);
                    If_and_loop.ParsIf(realcommandto.TrimStart(), poz, file, inputFilePath, func, errorcount, false);
                    //File.AppendAllText(file, $"\n.looopLC00h7{realpoz}:\n");
                    //If_and_loop.Parswhile("while " + realcommandto.TrimStart().Substring(3), poz, file, inputFilePath, func, errorcount);
                    //List<string> lines = File.ReadAllLines(file).ToList();
                    //for (int i = lines.Count() - 1; i > 0; i--)
                    //{
                    //    if (lines[i] == $".looopLC00h7{realpoz}:")
                    //    {
                    //        lines[i] = "";
                    //        break;
                    //    }

                    //}

                    //File.WriteAllLines(file, lines);
                    //File.AppendAllText(file, $"\njmp .whileLC025{lastif}\n.looopLC00h7{realpoz}:\n");
                    inif = true; continue;
                }
                else if (realcommandto.TrimStart().StartsWith("foreach "))
                {
                    inforeach = true;
                    preorityloop.Add("foreach");
                    brasecouforeach++;
                    string[] a = realcommandto.TrimStart().Replace("@", " ").Substring(8).Split(',');
                    int isp = Parser.isperemen(a[1].Trim().Replace(" ", null), peremen, typeperemen);
                    File.AppendAllText(file, "\n" + $"mov cx,{a[2]}\nmov ebx,0\n.foreach{realpoz}LC00:\npush cx");
                    foreachstring = $".foreach{realpoz}LC00";
                    if (typeperemen[isp] == "byte")
                    {
                        File.AppendAllText(file, $"\nlea edx, [{a[1]}  + ebx]\r\n mov al,[edx]\nmov [{a[0]}],al");
                    }
                    else if (typeperemen[isp] == "word") File.AppendAllText(file, $"\nlea edx, [{a[1]} + 2 * ebx]\r\n  mov ax,[edx] \n   mov [{a[0]}],ax");
                    else if (typeperemen[isp] == "dword") File.AppendAllText(file, $"\nlea edx, [{a[1]} + 4 * ebx]\r\n mov eax,[edx] \n  mov [{a[0]}],eax"); 
                    continue;
                }
                else if (realcommandto.TrimStart().StartsWith("for "))
                {
                    preorityloop.Add("for");
                    lastfor++;
                    brasecoufor++;
                    File.AppendAllText(file, $"\n.looopLC00{realpoz}:\n");
                    If_and_loop.Parsfor("for " + realcommandto.TrimStart().Substring(4), poz, file, inputFilePath, func, errorcount, peremen, typeperemen, false);
                    confor.Add(If_and_loop.Parsfor("for " + realcommandto.TrimStart().Substring(4), poz, file, inputFilePath, func, errorcount, peremen, typeperemen, true));
                    List<string> lines = File.ReadAllLines(file).ToList();
                    for (int i = 0; i < lines.Count(); i++)
                    {
                        if (lines[i] == $".looopLC00{realpoz}:")
                        {
                            lines[i] = "";

                        }

                    }

                    File.WriteAllLines(file, lines);
                    File.AppendAllText(file, $"\njmp .foreeLC0{lastfor}\n.looopLC00{realpoz}:\n");
                    forcmp = $"jmp .foreeLC0{lastfor}";
                    infor = true; continue;
                }
                else if (realcommandto.Trim().StartsWith("}"))
                {
                    if (instrict) { instrict = false; }
                    if (inforeach && preorityloop[preorityloop.Count - 1] == "foreach")
                    {
                        File.AppendAllText(file, $"\n inc ebx\npop cx\ndec cx\njcxz {foreachstring}");
                        brasecouforeach--;
                        if (brasecouforeach == 0) inforeach = false;
                        preorityloop.RemoveAt(preorityloop.Count() - 1);

                        wasbrk = false;
                        foreach (var item in lastnameforeach)
                        {
                            File.AppendAllText(file,"\n"+ item);
                        }

                        lastnameforeach.Clear();



                    }
                    else if (infor && preorityloop[preorityloop.Count - 1] == "for")
                    {
                        File.AppendAllText(file, confor[confor.Count - 1] + "\n" + forcmp.Substring(4) + ":");

                        confor.RemoveAt(confor.Count() - 1);
                        brasecoufor--;
                        if (brasecoufor == 0) infor = false;

                        preorityloop.RemoveAt(preorityloop.Count() - 1);

                        wasbrk = false;
                        foreach (var item in lastnamefor)
                        {
                            File.AppendAllText(file, item);
                        }

                        lastnamefor.Clear();


                    }
                    else if (inwhile && preorityloop[preorityloop.Count - 1] == "while")
                    {
                        // string[] lines = File.ReadAllLines(file);
                        // string a = null;
                        File.AppendAllText(file, "\n.endw");

                        brasecouwhile--;
                        if (brasecouwhile == 0) inwhile = false;
                        preorityloop.RemoveAt(preorityloop.Count() - 1);

                        wasbrk = false;
                        foreach (var item in lastnamewhile)
                        {
                            File.AppendAllText(file, item);
                        }

                        lastnamewhile.Clear();

                        //  preorityloopindex.RemoveAt(preorityloopindex.Count() - 1);
                        //preorityloop.RemoveAt(preorityloop.Count() - 1);
                        // File.AppendAllText(file, $"\n.whileLC025{lastwhile}:");
                    }
                    else if (indowhile && preorityloop[preorityloop.Count - 1] == "dowhile")
                    {
                        // string[] lines = File.ReadAllLines(file);
                        // string a = null;
                        string a = watcondo[watcondo.Count() - 1].TrimStart();
                        If_and_loop.ParsIf("enddw " + a, poz, file, inputFilePath, func, errorcount, true);
                        watcondo.RemoveAt(watcondo.Count() - 1);
                        brasecoudowhile--;
                        if (brasecoudowhile == 0) indowhile = false;
                        preorityloop.RemoveAt(preorityloop.Count() - 1);

                        wasbrk = false;
                        foreach (var item in lastnamedowhile)
                        {
                            File.AppendAllText(file, item);
                        }

                        lastnamedowhile.Clear();

                        //  preorityloopindex.RemoveAt(preorityloopindex.Count() - 1);
                        //preorityloop.RemoveAt(preorityloop.Count() - 1);
                        // File.AppendAllText(file, $"\n.whileLC025{lastwhile}:");
                          
                    }
                    //else if (indowhile && preorityloop[preorityloop.Count - 1] == "dowhile")
                    //{
                    //    string[] lines = File.ReadAllLines(file);
                    //    string a = null;
                    //    for (int i = lines.Count() - 1; i >= 0; i--)
                    //    {

                    //        if (lines[i].EndsWith(":") && lines[i].Contains(".loopcLC00h7"))
                    //        {

                    //            a = lines[i].Replace(":", null);

                    //            break;
                    //        }

                    //    }
                    //    File.AppendAllText(file, "\n" + $"\n{watconwhiledo}");
                    //    brasecoudowhile--;
                    //    if (brasecoudowhile == 0) indowhile = false;
                    //    foreach (var item in watconwhilrdo)
                    //    {
                    //        File.AppendAllText(file, $"\n{item} {a}");
                    //    }
                    //    watconwhilrdo.Clear();
                    //    preorityloop.RemoveAt(preorityloop.Count() - 1);
                    //    File.AppendAllText(file, $"\n.whileLC025{lastwhiledo}:");
                    //}
                    else if (inif && preorityloop[preorityloop.Count - 1] == "if")
                    {
                        File.AppendAllText(file, "\n.endif");
                        //string[] lines = File.ReadAllLines(file);
                        //string a = null;
                        //for (int i = lines.Count() - 1; i >= 0; i--)
                        //{

                        //    if (lines[i].EndsWith(":") && lines[i].Contains(".looopLC00h7"))
                        //    {

                        //        a = lines[i].Replace(":", null);

                        //        break;
                        //    }

                        //}
                        brasecouif--;
                        if (brasecouif == 0) inif = false;
                        preorityloop.RemoveAt(preorityloop.Count() - 1);
                        //  File.AppendAllText(file, $"\n.whileLC025{preorityloopindex[preorityloopindex.Count - 1]}:");
                        // preorityloopindex.RemoveAt(preorityloopindex.Count() - 1);
                    }
                    else
                    {

                        int currentPosition = poz;
                        var lines = File.ReadAllLines(inputFilePath);
                        string fileContent = File.ReadAllText(file);
                        string[] lines2 = fileContent.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
                        using (StreamWriter outputFile = new StreamWriter(file, true))
                        {

                            outputFile.WriteLine("\njmp endOasm");

                        }
                    }
                }
                else if (realcommandto.TrimStart().StartsWith("struct "))
                {
                    instrict = true;
                    var lines = File.ReadAllLines(inputFilePath);
                    string isp = null;
                    File.AppendAllText(file, "\n" + realcommandto.Replace("struct ".Trim(), "struc ")); continue;

                }
                else if (realcommandto.TrimStart().StartsWith("inter "))
                {
                    string[] parts = realcommandto.TrimStart().Split(new char[] { ' ' }, 2, StringSplitOptions.RemoveEmptyEntries);
                    File.AppendAllText(file, "\nINT " + parts[1]); continue;
                }
                else if (realcommandto.TrimStart().StartsWith("import "))
                {
                    try
                    {


                        string command2 = realcommandto.Replace("import ", ""); command2 = command2.Replace("\"", ""); command2 = command2.Replace("\"", "");
                        string name = command2;
                        string[] command3 = File.ReadAllLines(command2.TrimStart());
                        if (realcommandto.Contains(".inc") || realcommandto.Contains(".asm"))
                        {
                            File.AppendAllText(file, $"\njmp main\n{realcommandto.Replace("import ", "include ")}\n;Import state is not state");
                            string[] commands2 = File.ReadAllLines(name.TrimStart());
                            for (int i1 = 0; i1 < command3.Count(); i1++)
                            {
                                if (command3[i1].EndsWith(':'))
                                {
                                    func.Add(command3[i1].Replace(":", "").Trim());
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


                                    string[] spl = command3[i1].TrimStart().Split("macro ", 2);
                                    string[] macro = spl[1].Split(' ', 2);

                                    macroses.Add($"{macro[0].Trim()}");
                                }
                                else if (command3[i1].TrimStart().StartsWith("struc "))
                                {


                                    string[] spl = command3[i1].TrimStart().Split("struc ", 2);
                                    struc.Add($"{spl[1].Trim()}");
                                }
                                else if (command3[i1].TrimStart().StartsWith("proc "))
                                {


                                    string[] spl = command3[i1].TrimStart().Split("proc ", 2);
                                    string[] macro = spl[1].Split(' ', 2);

                                    func.Add($"{macro[0].Trim()}");
                                }
                                else if (command3[i1].TrimStart().StartsWith("extrn "))
                                {
                                    func.Add(command3[i1].Substring(6).Trim());
                                }
                                }
                                continue;
                        }
                        else
                        {
                            File.AppendAllText(file, $"\njmp main\n{realcommandto.Replace("import ", "include ").Replace(".lum", ".asm")}\n;Import state is not state");
                            Process process2 = new Process();
                            process2.StartInfo.FileName = "lumin.exe";
                            process2.StartInfo.UseShellExecute = false;
                            process2.StartInfo.RedirectStandardOutput = true;
                            process2.StartInfo.RedirectStandardError = true;

                            string command9 = realcommandto.Replace("import ", ""); command2 = command2.Replace("\"", ""); command2 = command2.Replace("\"", "");
                            string name9 = command9;
                            process2.StartInfo.Arguments = name9 + " y";

                            process2.Start();
                            process2.WaitForExit();
                            helpfunc.ParsImportlum(command9.Replace(".lum", ".asm").Replace("\"", null), file, func, peremen, typeperemen, macroses, struc);
                        }
                    }
                    catch (Exception)
                    {
                        
                    }

                }
                else if (realcommandto.TrimStart().StartsWith("dllimport "))
                {
                    string a = realcommandto.TrimStart().Substring(10).Replace(" ", null);
                    string[] b = a.Trim().Split(',');
                    importhead += "," + b[0].Replace("'", null).Replace("\"", null).Replace(".dll", null) + ',' + b[0].Trim();
                    importside += $"\nimport {b[0].Replace("'", null).Replace("\"", null).Replace(".dll", null)} ,\\";
                    for (int i = 1; i < b.Count() - 1; i++)
                    {
                        importside += $"\n{b[i]},'{b[i]}',\\";
                    }
                    importside += $"\n{b[b.Count() - 1]},'{b[b.Count() - 1]}'";

                    for (int i = 1; i < b.Count(); i++)
                    {
                        dllfunc.Add(b[i].Trim());
                        func.Add(b[i].Trim());
                        // Console.WriteLine(b[i].Trim() + " d");
                    }
                    continue;
                }
                else if (realcommandto.TrimStart().StartsWith("proc "))
                {

                    File.AppendAllText(file, $"\n {realcommandto.TrimStart().Substring(5).Insert(0, "proc ").Replace("(", " ").Replace(" as "," :").Replace(")", null)}\n push    ebp\r\n        mov     ebp, esp");
                    string[] spl = realcommandto.Replace("dword ", null).Replace("word ", null).Split("proc", 2);
                    string[] macro = spl[1].Split('(');
                    func.Add($"{macro[0].Trim()}"); continue;
                    // Console.WriteLine($"{macro[0].Trim()}" + "fdfsdfsdfsdfsd");
                }
                else if (realcommandto.TrimStart().StartsWith("macro "))
                {

                    File.AppendAllText(file, $"\n {realcommandto.TrimStart().Substring(6).Insert(0, "macro ").Replace("(", " ").Replace(")", null)}");
                    string[] spl = realcommandto.Replace("dword ", null).Replace("word ", null).Split("macro", 2);
                    string[] macro = spl[1].Split('(');
                    macroses.Add($"{macro[0].Trim()}"); continue;
                    //  Console.WriteLine($"{macro[0].Trim()}" + "fdfsdfsdfsdfsd");
                }
                else if (realcommandto.TrimStart().StartsWith("delete "))
                {
                    string[] sp = realcommandto.TrimStart().Substring(7).Split(',', 2);

                    for (int i = 0; i < int.Parse(sp[1]); i++)
                    {
                        File.AppendAllText(file, $"\nmov [{sp[0]}+{i}],0");
                    }
                    File.AppendAllText(file, $"\nadd esp,{sp[1]}"); continue;

                }
                else if (realcommandto.TrimStart().StartsWith("func "))
                {
                    string command2 = realcommandto.Replace("func ", "");
                    string isp = null;
                    if (command2.Contains("{")) { command2 = command2.Replace("{", ""); }
                    File.AppendAllText(file, "\n" + command2 + ":");
                    int currentPosition = poz;
                    string fileContent = File.ReadAllText(file);
                    string[] lines2 = fileContent.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
                    File.WriteAllLines(file, lines2);
                    func.Add(command2.Replace(" ", "").Trim()); continue;

                }
                else if (realcommandto.TrimStart().StartsWith("defbyte "))
                {
                    File.AppendAllText(file, realcommandto.TrimStart().Replace("defbyte ", " db ")); continue;
                }
                else if (realcommandto.TrimStart().StartsWith("format "))
                {
                    string[] k = realcommandto.TrimStart().Split(" ");
                    if (k[1].Trim() == "winconsole".Trim()) { File.AppendAllText(file, "\nformat PE Console\n\ninclude 'fasm\\INCLUDE\\MACRO\\PROC32.INC'\n\ninclude 'fasm\\INCLUDE\\win32a.INC'\nentry main\njmp main\nformatString00 db \"%s\", 0\ncls000 db 'cls',0\nmacro allocatem size\ninvoke malloc,size\n}\nmacro freem size\ninvoke free,size\n}"); console = true; macroses.Add("allocatem"); macroses.Add("freem"); }

                    else if (k[1].Trim() == "linconsole".Trim()) { File.AppendAllText(file, "\nformat ELF executable\ninclude 'fasm\\INCLUDE\\MACRO\\PROC32.INC'\ninclude 'fasm\\INCLUDE\\MACRO\\import32.INC' \njmp main\nmacro allocatem size\nmov eax, 192              \r\nxor ebx, ebx              \r\nmov ecx, size            \r\nmov edx, 3                \r\nmov edi, 34               \r\nxor esi, esi              \r\nxor ebp, ebp              \r\nint 0x80                  \r\nmov edi, eax\n}\nmacro freem size\nstdcall strlen,[size]\nmov ecx,eax \nmov eax, 91       \r\n    mov ebx, [size] \r\n    int 0x80\n} \nproc sleep cou\r\n    mov eax, 62\r\n    mov ebx,[cou]\r\n    int 0x80 \r\nendp\r\nproc exit ind\r\nmov eax,[ind]\r\n xor ebx,ebx\r\n int 0x80\r\nendp\r\nproc waitforkeyexit ind\r\nmov eax,[ind]\r\nxor ebx,ebx \r\nint 0x80\r\nendp\r\nproc input perem\r\nmov eax, 3 \r\n   mov ebx, 0   \r\n   mov ecx,[perem]  \r\n   mov edx, 256\r\nint 0x80\r\nendp"); linconsole = true; macroses.Add("allocatem"); macroses.Add("freem"); }
                    else if (k[1].Trim() == "wingui".Trim())
                    {
                        File.AppendAllText(file, "\nformat PE GUI \ninclude 'fasm\\INCLUDE\\MACRO\\PROC32.INC'\ninclude 'fasm\\INCLUDE\\win32a.INC'\njmp main\nmacro allocatem size\n{\ninvoke malloc,size\n}\nmacro freem size\n{\ninvoke free,size\n}"); macroses.Add("allocatem"); macroses.Add("freem");
                        wingui = true;
                    }
                    else if (k[1].Trim() == "bios".Trim())
                    {
                        File.AppendAllText(file, "\ninclude 'fasm\\INCLUDE\\MACRO\\PROC32.INC'\n include 'fasm\\INCLUDE\\MACRO\\import32.INC'  \r\n cli\r\n        xor ax,ax\r\n        mov ds,ax\r\n        mov es,ax\r\n        mov ss,ax\r\n        mov sp,07C00h\r\n        sti\r\n     mov ah, 0x00    \r\n    mov al, 0x03   \r\n    int 0x10       \r\n\r\n   \r\n    mov ax, 0x03\r\n    int 0x10\nxor ax,ax\njmp main ");
                        boot = true;
                    }
                    else if (k[1].Trim() == "nonos".Trim())
                    {
                        //File.AppendAllText(file, "\n cli\r\n        xor ax,ax\r\n        mov ds,ax\r\n        mov es,ax\r\n        mov ss,ax\r\n        mov sp,07C00h\r\n        sti\r\n      ");
                        nonos = true;

                    }
                    else { File.AppendAllText(file, "\n" + realcommandto.TrimStart() + "\ninclude 'fasm\\INCLUDE\\MACRO\\PROC32.INC'\n include 'fasm\\INCLUDE\\MACRO\\import32.INC' \njmp main"); }
                    File.AppendAllText(file, "\n" + "bytesz equ 1\nwordsz equ 2\ndwordsz equ 4\ninclude 'fasm/include/macro/if.inc'"); continue;

                }
                else if (realcommandto.Trim().Replace(" ", null).StartsWith("mode16"))
                {
                    File.AppendAllText(file, "\nuse16");
                    mode = "16";
                    continue;
                }
                else if (realcommandto.Trim().Replace(" ", null).StartsWith("mode64"))
                {
                    mode = "64";
                    File.AppendAllText(file, "\nuse64"); continue;
                }
                else if (realcommandto.TrimStart().StartsWith("comparestr "))
                {
                    string command21 = realcommandto.Replace("comparestr ", "");
                    string isp1 = null;
                    string pattern = @"\d+";
                    string[] h = command21.Split("then");
                    string[] partsw = h;
                    string[] nums = partsw[0].Split(',', 2);
                    string[] nums2 = nums[1].Split(',', 2);
                    string[] nums3 = nums2[1].Split(',', 2);
                    bool was1 = false;
                    string[] lines = File.ReadAllLines(inputFilePath);
                    string a = null;
                    if (char.IsLetter(nums3[0][0])) { File.AppendAllText(file, $"\n   mov si,[{nums[0]}] \r\n    mov di,[{nums2[0]}]\r\n    mov cx,[{nums3[0]}]\r\n   push si\r\n    push di\r\n    push cx\ncycle{last}:\r\n    mov al, [si]\r\n    cmp al, [di]\r\n    jnz {nums3[1]}\r\n    inc si\r\n    inc di\r\n    loop cycle{last}\r\n  pop cx\r\n    pop di\r\n    pop si\n   jmp {h[1]}  "); }
                    else
                    {
                        File.AppendAllText(file, $"\n   mov si,[{nums[0]}] \r\n    mov di,[{nums2[0]}]\r\n    mov cx,{nums3[0]}\r\n   push si\r\n    push di\r\n    push cx\ncycle{last}:\r\n    mov al, [si]\r\n    cmp al, [di]\r\n    jnz {nums3[1]}\r\n    inc si\r\n    inc di\r\n    loop cycle{last}\r\n  pop cx\r\n    pop di\r\n    pop si\n   jmp {h[1]}  ");
                    }
                    last++;
                    continue;
                }
                else if (realcommandto.TrimStart().StartsWith("sector ") && !realcommandto.TrimStart().StartsWith("sector end"))
                {
                    string[] parts = realcommandto.TrimStart().Split(' ', 2, StringSplitOptions.RemoveEmptyEntries);
                    if (parts.Length == 2)
                    {
                        File.AppendAllText(file, "\n" + "org " + parts[1]);
                    }
                    else
                    {
                    }
                    continue;
                }
                else if (realcommandto.TrimStart().StartsWith("sector end"))
                {
                    boot = true;
                    ferifyboot = true;

                }
                else if (realcommandto.TrimStart().StartsWith("locals"))
                {
                    File.AppendAllText(file, "\nlocals"); continue;
                }
                else if (realcommandto.TrimStart().StartsWith("reserve"))
                {
                    string[] parts = realcommandto.TrimStart().Split(new char[] { ' ' }, 2, StringSplitOptions.RemoveEmptyEntries);
                    string[] t = parts[1].Split(new char[] { ',' }, 2, StringSplitOptions.RemoveEmptyEntries);
                    string pattern = @"\d+";
                    string parsed = null;
                    if (t[0].Contains("ubyte")) { parsed = "du 0"; }
                    if (t[0].Contains("byte")) { parsed = "db 0"; }

                    if (t[0].Contains("word")) { parsed = "dw 0"; }
                    if (t[0].Contains("dword")) { parsed = "dd 0"; }
                    if (t[0].Contains("qword")) { parsed = "dq 0"; }
                    if (t[0].Contains("tword")) { parsed = "dt 0"; }
                    parsed = $"\ntimes {t[1]} " + parsed;
                    File.AppendAllText(file, parsed); continue;

                }
                else if (realcommandto.TrimStart().StartsWith("savestack"))
                {
                    string[] prt = realcommandto.TrimStart().Split(" ");
                    bool a = false;
                    for (int i = 0; i < peremen.Count; i++)
                    {
                        if (prt[1].Replace(" ", "") == peremen[i].Replace(" ", ""))
                        {
                            File.AppendAllText(file, $"\npush {prt[1]}");
                            a = true;
                            break;
                        }
                    }
                    if (a == false)
                    {
                        File.AppendAllText(file, $"\npush {prt[1]}");
                    }
                    continue;

                }
                else if (realcommandto.TrimStart().StartsWith("restorestack"))
                {
                    string[] prt = realcommandto.TrimStart().Split(" ");
                    bool a = false;
                    for (int i = 0; i < peremen.Count; i++)
                    {
                        if (prt[1].Replace(" ", "") == peremen[i].Replace(" ", ""))
                        {
                            File.AppendAllText(file, $"\npop [{prt[1]}]");
                            a = true;
                            break;
                        }
                    }
                    if (a == false)
                    {
                        File.AppendAllText(file, $"\npop {prt[1]}");
                    }
                    continue;

                }
                else if (realcommandto.TrimStart().StartsWith("crttemp"))
                {
                    int ied = 0;
                    int pointof2 = 0;
                    bool pop = false;
                    string[] srtc = realcommandto.TrimStart().Split(' ', 2);
                    string[] srtc2 = srtc[1].TrimStart().Split(',', 2);
                    string[] p = srtc2[1].Split("(", 2);
                    List<string> all = p[0].Split(',').ToList();
                    string[] fil = File.ReadAllLines(file);
                    List<string> resultLines = new List<string>();
                    for (int i = 0; i < classn.Count; i++)
                    {
                        if (srtc2[0].TrimStart().StartsWith(classn[i].TrimStart()) && !int.TryParse(srtc2[1].Trim().Substring(0, srtc2[1].Trim().Length - 1), out int y))
                        {



                            for (int ia = 0; ia < fil.Count() - 1; ia++)
                            {
                                // Console.WriteLine("ddd");
                                if (ia == i)
                                {

                                    pointof2 = i;
                                    string[] lines = File.ReadAllLines(file);

                                    bool isCapturing = false;

                                    foreach (string line in lines)
                                    {

                                        string trimmedLine = line;

                                        // Console.WriteLine(trimmedLine);
                                        if (trimmedLine.StartsWith($";class {classn[i]}"))
                                        {


                                            isCapturing = true;
                                            resultLines.Add(trimmedLine);
                                            continue;
                                        }
                                        else if (isCapturing && trimmedLine.TrimStart().StartsWith("proc "))
                                        {

                                            var methodName = trimmedLine.TrimStart().Substring(6);

                                            string[] spl = trimmedLine.Split("proc", 2);
                                            string[] macro = methodName.Split(' ', 2);
                                            func.Add($"{macro[0].Trim().Replace($"{classn[i]}.", $"{all[all.Count() - 1]}.")}");
                                            // Console.WriteLine(all[all.Count() - 1]);
                                            // Console.WriteLine("ddasdasdasdasdd " + $"{macro[0].Trim().Replace($"{classn[i]}.", $"{srtc[1]}.")}");
                                            //  whatin.Add($"\nfunc {className}.{methodName}");

                                        }
                                        else if (isCapturing && trimmedLine.TrimStart().StartsWith("macro "))
                                        {

                                            var methodName = trimmedLine.TrimStart().Substring(6);

                                            string[] spl = trimmedLine.Split("macro", 2);
                                            string[] macro = methodName.Split(' ', 2);
                                            macroses.Add($"{macro[0].Trim().Replace($"{classn[i]}.", $"{all[all.Count() - 1]}.")}");
                                            // Console.WriteLine("ddasdasdasdasdd " + $"{macro[0].Trim().Replace($"{classn[i]}.", $"{srtc[1]}.")}");
                                            //  whatin.Add($"\nfunc {className}.{methodName}");

                                        }
                                        else if (isCapturing && trimmedLine.TrimStart().EndsWith(":"))
                                        {

                                            var methodName = trimmedLine.Trim();

                                            func.Add($"{methodName.Replace(":", null).Replace($"{classn[i]}.", $"{all[all.Count() - 1]}.")}");



                                            // Console.WriteLine($"hh {methodName.Replace(":", null).Replace($"{classn[i]}.", $"{srtc[1]}.")}");
                                        }
                                        if (isCapturing)
                                        {
                                            if (trimmedLine.StartsWith($";class ends"))
                                            {
                                                // Console.WriteLine($";class ends");
                                                resultLines.Add(trimmedLine);
                                                pop = true;

                                                break;
                                            }
                                            resultLines.Add(trimmedLine);


                                        }
                                    }





                                }

                            }

                        }
                        //else { File.AppendAllText(file, "\n" + srtc[1] + " " + srtc[0]);
                        // Console.WriteLine("dddddddddddddddddddddddddddddddddddddddddddddddddddddddd"); 
                        // }
                        fil = File.ReadAllLines(file);
                        int f = 0;
                        int f4 = 0;

                        for (int f1 = 0; f1 < fil.Count(); f1++)
                        {
                            if (fil[f1].TrimStart().StartsWith($";class ends"))
                            {
                                f = f1;
                                break;
                            }
                            if (fil[f1].TrimStart().StartsWith($";point"))
                            {
                                f4 = f1;
                                break;
                            }
                        }
                        for (int j = 0; j < resultLines.Count; j++)
                        {
                            if (pop)
                            {
                                // Console.WriteLine($"{classn[i].Replace(" ", null)}.");
                                fil[f + 1] = fil[f + 1] + "\n" + resultLines[j].Replace($"{classn[i].Replace(" ", null)}.", $"{all[all.Count() - 1].Replace(" ", null).Replace("(", null).Replace(")", null)}.").Replace($";class {classn[i]}", $";class {all[all.Count() - 1].Replace("(", null).Replace(")", null).Replace(" ", null)}");
                                // Console.WriteLine("f" + fil[f]);
                            }
                        }

                    }
                    for (int j = 0; j < fil.Count(); j++)
                    {
                        if (j == fil.Count() - 1 && pop)
                        {

                            int openBracketIndex = srtc[1].IndexOf('(');
                            int closeBracketIndex = srtc[1].Trim().IndexOf(')');
                            int commaIndex = srtc[1].IndexOf(',');

                            string[] a = srtc[1].Split(",", 2);
                            string parameters = srtc[1].Substring(openBracketIndex).Trim();

                            string[] constructor = a[1].Split("(", 2);
                            string result = null;

                            string[] parts = parameters.Trim().Substring(1, parameters.Length - 2).Split(',');
                            if (parameters.Contains(","))
                            {
                                for (int i = 0; i < parts.Length; i++)
                                {
                                    parts[i] = helpfunc.parsstring(parts[i], true,stringstoaddend,stringlasted);
                                    if (char.IsLetter(parts[i].Trim()[0]) && parts[i].Trim() != "" && parts[i].Trim() != "''" && !parts[i].Trim().StartsWith("'") && !parts[i].Trim().StartsWith("\"") && parts[i].Trim() != "''" && parts[i].Trim() != "\"\"")
                                    { parts[i] = $"[{parts[i]}]"; }

                                }
                                string g = string.Join(",", parts);

                                result = $"\nstdcall {constructor[0].Replace(" ", null).Replace("(", null).Replace(")", null)}.constructing,{g}";
                            }
                            else
                            {
                                result = $"\nstdcall {constructor[0].Replace(" ", null).Replace("(", null).Replace(")", null)}.constructing";
                            }


                            fil[j] += result;

                            break;
                        }
                    }

                    File.WriteAllLines(file, fil);
                    classn.Add(all[all.Count() - 1].Replace("(", null).Replace(")", null).Replace(" ", null));
                    continue;
                }
                else if (realcommandto.TrimStart().StartsWith("return "))
                {
                    string pattern = @"\d+";
                    if (realcommandto.Trim() == "return")
                    {
                        File.AppendAllText(file, "\n" + $"ret");
                    }
                    else
                    {
                        string[] prt = realcommandto.Split(" ", 2, StringSplitOptions.RemoveEmptyEntries);
                        bool containsNumbers = false;
                        try
                        {
                            containsNumbers = Regex.IsMatch(prt[1], pattern);
                        }
                        catch { }

                        if (char.IsLetter(prt[1].TrimStart()[0]))
                        {
                            File.AppendAllText(file, "\n" + $"mov eax,[{prt[1]}]\nret");
                        }
                        else
                        {
                            Parscall(file, prt[1], func, realpoz, peremen, typeperemen, macroses, dllfunc, true, false);
                            File.AppendAllText(file, "\n" + $"mov eax,{prt[1]}\nret");
                            break;
                        }


                    }
                    continue;
                }
                else if (realcommandto.TrimStart().StartsWith("locallabel "))
                {
                    File.AppendAllText(file, "\nlocal " + realcommandto.TrimStart().Substring(10)); continue;
                }
                else if (realcommandto.TrimStart().StartsWith("initproc "))
                {
                    func.Add(realcommandto.TrimStart().Substring(9).Trim()); continue;
                }
                else if (realcommandto.TrimStart().StartsWith("initdllfunc "))
                {
                    dllfunc.Add(realcommandto.TrimStart().Substring(12).Trim()); continue;
                }
                else if (realcommandto.TrimStart().StartsWith("initmacro "))
                {
                    macroses.Add(realcommandto.TrimStart().Substring(10).Trim()); continue;
                }
                else if (command.TrimStart().StartsWith("entrypoint"))
                {
                    string[] parts = command.TrimStart().Split(new char[] { ' ' }, 2, StringSplitOptions.RemoveEmptyEntries);
                    File.AppendAllText(file, "\n" + "entry " + parts[1]); continue;
                }
                else if (realcommandto.Trim() == "stop")
                {
                    File.AppendAllText(file, "\n" + $"jmp endOasm");
                    continue;
                }
                else if (realcommandto.TrimStart().StartsWith("loop "))
                {
                    If_and_loop.ParsLoop(realcommandto, poz, file, inputFilePath, func, errorcount); continue;
                }
                else if (realcommandto.Trim() == "asm")
                {
                    wasassemb = true;
                    continue;
                }
                else if (realcommandto.TrimStart().StartsWith("crtstruc"))
                {
                    string[] srtc = realcommandto.TrimStart().Split(new char[] { ' ' }, 2, StringSplitOptions.RemoveEmptyEntries);
                    string[] srtc2 = srtc[1].TrimStart().Split(new char[] { ',' }, 2, StringSplitOptions.RemoveEmptyEntries);

                    File.AppendAllText(file, $"\n {srtc2[1]} {srtc2[0]}");
                    continue;
                }
                else if (realcommandto.TrimStart().StartsWith("goto "))
                {
                    string h = realcommandto.TrimStart().Substring(5);
                    File.AppendAllText(file, "\njmp " + h); continue;
                }
                else if (realcommandto.TrimStart().StartsWith("dword") || realcommandto.TrimStart().StartsWith("stack ") || realcommandto.TrimStart().StartsWith("word") || realcommandto.TrimStart().StartsWith("tword") || realcommandto.TrimStart().StartsWith("qword") || realcommandto.TrimStart().StartsWith("byte") || realcommandto.TrimStart().StartsWith("ubyte"))
                {
                    types.ParsTypes(realcommandto, file, peremen, typeperemen, func, macroses, errorcount, errorcount, laststack, instrict, dllfunc,stringstoaddend,stringlasted);
                    continue;
                }
                //else if (realcommandto.TrimStart().StartsWith("entrypoint") || realcommandto.TrimStart().StartsWith("callwinapi") || realcommandto.TrimStart().StartsWith("setencoding") || realcommandto.TrimStart().StartsWith("freem") || realcommandto.TrimStart().StartsWith("allocatem") || realcommandto.TrimStart().StartsWith("strcmp") || realcommandto.TrimStart().StartsWith("section") || realcommandto.TrimStart().StartsWith("sleep ") || realcommandto.TrimStart().StartsWith("exit") || realcommandto.TrimStart().StartsWith("printl ") || realcommandto.TrimStart().StartsWith("print ") || realcommandto.TrimStart().StartsWith("waitforkeyexit") || realcommandto.TrimStart().StartsWith("system") || realcommandto.TrimStart().StartsWith("clearscreen"))
                //{
                //    wccommands.Parsccom(realcommandto, file, inputFilePath, last, linconsole, boot, console, realpoz, errorcount, wingui);


                //}
                //else if (realcommandto.TrimStart().StartsWith("section") || realcommandto.TrimStart().StartsWith("syscall") || realcommandto.TrimStart().StartsWith("sleep") || realcommandto.TrimStart().StartsWith("setcurpoz") || realcommandto.TrimStart().StartsWith("print ") || realcommandto.TrimStart().StartsWith("input") || realcommandto.TrimStart().StartsWith("textcolor") || realcommandto.TrimStart().StartsWith("setcursize"))
                //{
                //    wccommands.Parsccom(realcommandto, file, inputFilePath, last, linconsole, boot, console, realpoz, errorcount, wingui);

                //}
                //else if (realcommandto.TrimStart().StartsWith("arrayadd") || realcommandto.TrimStart().StartsWith("arraydelete") || realcommandto.TrimStart().StartsWith("wcprintarray"))
                //{
                //    array.ParsArray(realcommandto, file, last, realpoz);

                //}
                //else if (realcommandto.Trim().StartsWith("beep") || realcommandto.Trim().StartsWith("print ") || realcommandto.TrimStart().StartsWith("wingui.createwindow ") || realcommandto.TrimStart().StartsWith("setcursize") || realcommandto.TrimStart().StartsWith("setcurpoz") || realcommandto.Trim().StartsWith("beep"))
                //{
                //    st = wccommands.Parsccom(realcommandto, file, inputFilePath, last, linconsole, boot, console, realpoz, errorcount, wingui);

                //}
                else if (!realcommandto.TrimStart().StartsWith("*") && !realcommandto.TrimStart().StartsWith("define") && !realcommandto.Trim().EndsWith("\n{"))
                {
                    bool chek = Parscall(file, realcommandto, func, realpoz, peremen, typeperemen, macroses, dllfunc, false, true);
                    if (!chek)
                    {
                        for (int i = 0; i < dllfunc.Count; i++)
                        {
                            if (realcommandto.TrimStart() == dllfunc[i] + " ")
                            {

                                File.AppendAllText(file, "\ninvoke" + realcommandto);
                                break;
                            }
                        }

                        for (int i = 0; i < func.Count; i++)
                        {

                            if (realcommandto.TrimStart() == func[i])
                            {
                                File.AppendAllText(file, "\nstdcall " + realcommandto);
                                break;
                            }
                        }

                        for (int i = 0; i < macroses.Count(); i++)
                        {

                            if (realcommandto.TrimStart() == macroses[i])
                            {

                                File.AppendAllText(file, "\n" + realcommandto);
                                break;
                            }
                        }
                    }
                    continue;
                }

            }
            else
            {
                if (realcommandto.TrimStart().StartsWith("}"))
                {
                    wasassemb = false;
                }
                else
                {
                    File.AppendAllText(file, "\n" + realcommandto);
                }

            }


        }

        string[] lst = File.ReadAllLines(file);
        bool wasDifune = false;

        var patterns = new List<string>();
        foreach (var define in other.definestack)
        {
            string pattern = @"(?<!['""])\b" + Regex.Escape(define) + @"\b(?!['""])";
            patterns.Add(pattern);
        }

        for (int i = 0; i < lst.Length; i++)
        {
            for (int u = 0; u < other.definestack.Count; u++)
            {
                int cou = Parser.isperemen(other.definestack[u], peremen, typeperemen);
                if (Regex.IsMatch(lst[i].Trim(), patterns[u]))
                {
                    lst[i] = Regex.Replace(lst[i], patterns[u], typeperemen[cou] + " " + other.watdefinestack[u]);
                    wasDifune = true;

                    string typeVar = typeperemen[cou] + " ";
                    lst[i] = helpfunc.RemoveExtraSpaces( lst[i]
                        .Replace($"[{typeVar}", $" {typeVar} [")
                        .Replace($"[  {typeVar}", $" {typeVar} [")
                        .Replace($"[{typeVar.Trim()}", $" {typeVar.Trim()} [")
                        .Replace($"[ {typeVar.Trim()}", $" {typeVar.Trim()} ["));
                }
            }
        }

        if (wasDifune)
        {
            File.WriteAllLines(file, lst);
        }
        foreach (var item in func)
        {
            Console.WriteLine(item);
        }
        //}
        //catch (Exception)
        //{

        //    Console.WriteLine("Error: Invalid global command syntax! On line: " + (poz + 1)); errorcount++;
        //}
        foreach (var item in stringstoaddend)
        {
            File.AppendAllText(file, "\n" + item);
        }
        if (wingui)
        {
            File.AppendAllText(file, "\n" + $"    xor eax,eax\r\n    jmp finish\r\n  .wmdestroy:\r\n    invoke PostQuitMessage,0\r\n    xor eax,eax\r\n\t\r\n  finish:\r\n    ret\r\n endp \n}}      \r\n \r\nstartgui:\nwingui.createwindow {st}");
        }
        else if (console || wingui)
        {
            File.AppendAllText(file, "\n" + $"section '.idata' data import readable\r\nlibrary kernel32, 'kernel32.dll', user32, 'user32.dll', advapi32,'advapi32.dll', msvcrt, 'msvcrt.dll'{importhead}\r\ninclude 'fasm/include/api/kernel32.inc'\ninclude 'fasm/include/api/user32.inc'\r\nimport msvcrt,\\\r\nprintf, 'printf',\\\r\nscanf, 'scanf',\\\r\ngetch, '_getch',\\\r\nsrand, 'srand',\\\r\nrand, 'rand',\\\r\nmalloc,'malloc',\\\r\n free,'free',\\\r\nsystem, 'system'" + importside);
        }
        else if (importhead != "")
        {
            File.AppendAllText(file, "\nsection '.idata' data import readable\nlibrary " + importhead.Substring(1) + "\n" + importside);
        }

        prepare.Prepare(file, boot, ferifyboot, mode);
        stopwatch.Stop();
        // if (errorcount != 0) { Console.WriteLine("Ошибки:"); }
        //Console.WriteLine($"Ошибок при интерпретации: {errorcount}");
        Console.WriteLine("Интерпретация завершена: " + stopwatch.ElapsedMilliseconds + " мс\nКомпиляция...");

        Process process = new Process();
        process.StartInfo.FileName = "fasm/fasm.exe";
        process.StartInfo.UseShellExecute = false;
        process.StartInfo.RedirectStandardOutput = true;
        process.StartInfo.RedirectStandardError = true;
        process.StartInfo.Arguments = file;

        process.Start();
        string errorOutput = process.StandardError.ReadToEnd();
        process.WaitForExit();
        if (close2) { Environment.Exit(0); }
        Console.WriteLine();
        string[] errorLines = errorOutput.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
        if (!string.IsNullOrEmpty(errorOutput))
        {
            for (int i = 0; i < errorLines.Count(); i++)
            {
                if (i == 0)
                {
                    string[] trim = errorLines[i].Split(" ", 2);
                    Console.WriteLine($"Ошибка в файле {trim[0]} на строке {trim[1].Replace("[", null).Replace("]", null)} ");


                    string[] p = { "ru" };

                    // a=  yandex.TranslateTextAsync(errorLines[3].Substring(7),"ru").ToString();
                    try
                    {


                        Console.Write(errorLines[1] + $" ({errorLines[3].Substring(7)})\n\n");
                    }
                    catch (Exception)
                    {


                    }
                }
            }
        }

        if (process.ExitCode == 0)
        {
            Console.WriteLine("Компиляция завершена!");
        }
        else { Console.WriteLine("Компиляция завершилась с ошибками!"); }
        Console.ReadKey();
        Environment.Exit(0);
    }


    public static bool Parscall(string file, string realcommandto, List<string> func, int realpoz, List<string> peremen, List<string> typeperemen, List<string> macroses, List<string> dllfunc, bool recur, bool canbus)
    {

        string[] srtc = null;
        string rtyu = "";

        srtc = realcommandto.TrimStart().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        if (realcommandto.TrimStart().Contains(' ')) { }
        else
        {
            rtyu = realcommandto.TrimStart();
        }

        bool ch = false;
        if (!recur) ch = Parser.Pars(realcommandto, file, func, realpoz, peremen, typeperemen, macroses, dllfunc,stringstoaddend,stringlasted);
        int ied = 0;
        int pointof2 = 0;
        bool pop = false;
        bool d = false;

        //func.Add("wingui.createbutton");
        if (!ch)
        {

            if (!d)
            {

              
                    bool ismacro = false;
                    string com = realcommandto.TrimStart();
                    string macro = null;
                    bool isstruc = false;
                    bool simp = false;
                    bool funci = false;
                    bool dllfun = false;
                    bool funcn = false;

                    macro = com.TrimStart();
                    string[] e = { };
                    if (com.TrimStart().Contains(" "))
                    {
                        e = macro.Split(' ', 2);
                        macro = e[0];
                    }
                    for (int i = 0; i < dllfunc.Count; i++)
                    {
                        if (com.TrimStart().StartsWith(dllfunc[i] + " "))
                        {

                            dllfun = true;
                            ismacro = false;
                            break;
                        }
                    }

                    for (int i = 0; i < func.Count; i++)
                    {

                        if (com.TrimStart().StartsWith(func[i] + " "))
                        {
                            ismacro = false;
                            com = macro;
                            funcn = true;
                            break;
                        }
                    }

                    for (int i = 0; i < macroses.Count(); i++)
                    {

                        if (com.TrimStart().StartsWith(macroses[i] + " "))
                        {
                            //Console.WriteLine(macroses[i] + " ddddd");

                            ismacro = true;
                            com = macro;
                            simp = true;
                            funci = true;
                            break;
                        }
                    }



                    if (com.TrimStart().Contains(" ") && !ismacro && !funci && !dllfun && !funcn)
                    {
                        isstruc = true;
                    }

                    if (!ismacro)
                    {
                        if (!isstruc)
                        {
                            //com = realcommandto.Replace(" ", null);
                            //if (com.TrimStart().Contains("."))
                            //{

                            //    string real = realcommandto.TrimStart();
                            //    int ind = real.TrimStart().IndexOf(' ');
                            //    char[] a = real.TrimStart().ToCharArray();
                            //    Console.WriteLine(real);
                            //    if (ind != -1)
                            //    {
                            //        a[ind] = ',';
                            //        srtc[0] = new String(a);
                            //        File.AppendAllText(file, "\n" + "stdcall " + srtc[0]);
                            //    }
                            //    else
                            //    {
                            //        File.AppendAllText(file, "\n" + "call " + srtc[0]);
                            //    }
                            //    break;

                            //}
                            // Console.WriteLine(com);
                            if (!ismacro && !funci ||  dllfun)
                            {

                                string real = realcommandto.TrimStart();
                                int ind = real.TrimStart().IndexOf(' ');
                                char[] a = real.TrimStart().ToCharArray();
                                // Console.WriteLine(real);
                                if (ind != -1)
                                { //File.AppendAllText(file, "\n" + "invoke " + realcommandto);

                                    a[ind] = ',';
                                    srtc[0] = new String(a);
                                    string[] parts = helpfunc.SplitString(srtc[0]).ToArray();
                                    int buffer = 0;
                                    string[] lin = File.ReadAllLines(file);
                                    int ccc = lin.Length - 1;
                                    for (int i = 1; i < parts.Length; i++)
                                    {
                                        if (parts[i].Length >= 1)
                                        {

                                            parts[i] = helpfunc.parsstring(parts[i], true,stringstoaddend,stringlasted);
                                            if (parts[i].TrimEnd().EndsWith(")"))
                                            {

                                                parts[i] = parts[i].TrimEnd().TrimStart().Substring(0, parts[i].TrimEnd().TrimStart().Length - 1);
                                                int ig = parts[i].IndexOf("(");
                                                parts[i] = parts[i].Remove(ig, 1).Insert(ig, " "); ////Console.WriteLine(parts[i]);


                                            }
                                            if (!Parscall(file, parts[i], func, realpoz, peremen, typeperemen, macroses, dllfunc, true, false))
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
                                    string g = string.Join(",", parts);
                                    if (!dllfun)
                                    {
                                        File.AppendAllText(file, "\n" + "stdcall " + g);
                                    }
                                    else
                                    {
                                        File.AppendAllText(file, "\n" + "invoke " + g);
                                    }

                                    return true;
                                }
                                //else
                                //{
                                //    if (ind != -1)
                                //    {

                                //        a[ind] = ',';
                                //        srtc[0] = new String(a);
                                //        string[] parts = SplitString(srtc[0]).ToArray();
                                //        int buffer = 0;

                                //        for (int i = 1; i < parts.Length; i++)
                                //        {
                                //            if (parts[i].Length >= 1)
                                //            {
                                //                parts[i] = parsstring(parts[i], true);
                                //                if (parts[i].TrimEnd().EndsWith(")"))
                                //                {

                                //                    parts[i] = parts[i].TrimEnd().TrimStart().Substring(0, parts[i].TrimEnd().TrimStart().Length - 1);
                                //                    int ig = parts[i].IndexOf("(");
                                //                    parts[i] = parts[i].Remove(ig, 1).Insert(ig, " "); ////Console.WriteLine(parts[i]);


                                //                }
                                //                if (!Parscall(file, parts[i], func, realpoz, peremen, typeperemen, macroses, dllfunc, true, false))
                                //                {
                                //                    if (char.IsLetter(parts[i].Trim().Replace(" ", null)[0]))
                                //                    { parts[i] = $"[{parts[i]}]"; }
                                //                }
                                //                else
                                //                {
                                //                    buffer += 4;
                                //                    File.AppendAllText(file, $"\nmov dword [ebp-{buffer}],eax");
                                //                    parts[i] = $"[ebp-{buffer}]";
                                //                }


                                //            }


                                //        }
                                //        string g = string.Join(",", parts);
                                //        if (!dllfun)
                                //        {
                                //            File.AppendAllText(file, "\n" + "stdcall " + g);
                                //        }
                                //        else
                                //        {
                                //            File.AppendAllText(file, "\n" + "invoke " + g);
                                //        }

                                //        return true;
                                //    }
                                //}
                               
                            }


                        }
                        else
                        {
                            try
                            {


                                File.AppendAllText(file, "\n" + srtc[1] + " " + srtc[0].Replace(")", " ").Replace("(", " ") + " " + srtc[2]);
                                return true;
                            }
                            catch (Exception)
                            {
                                return true;

                            }
                          
                        }
                    }
                    else
                    {
                        //   Console.WriteLine(macro + " " + func[ia]);


                        string[] h = realcommandto.TrimStart().Split(" ", 2);
                        string[] parts =helpfunc.SplitString(h[1]).ToArray();
                        int buffer = 0;
                        string[] lin = File.ReadAllLines(file);
                        int ccc = lin.Length - 1;
                        for (int i = 0; i < parts.Length; i++)
                    {
                        if (parts[i].Length >= 1)
                        {

                            if (parts[i].TrimEnd().EndsWith(")"))
                            {

                                parts[i] = parts[i].TrimEnd().TrimStart().Substring(0, parts[i].TrimEnd().TrimStart().Length - 1);
                                int ig = parts[i].IndexOf("(");
                                parts[i] = parts[i].Remove(ig, 1).Insert(ig, " "); ////Console.WriteLine(parts[i]);


                            }
                            if (!Parscall(file, parts[i], func, realpoz, peremen, typeperemen, macroses, dllfunc, true, false))
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
                        string g = string.Join(",", parts); if (buffer != 0)
                        {
                            File.AppendAllText(file, "\n" + "\n" + h[0] + " " + g);
                        }
                        else { File.AppendAllText(file, "\n" + "\n" + h[0] + " " + g); }
                        return true;





                    }
                
                string pattern = @"\d+";
            }
        }

        return false;

        return false;
    }
 
}






////    else if (realcommandto.TrimStart().StartsWith("dowhile "))
//    {
//        lastwhiledo++;
//        brasecoudowhile++;
//        File.AppendAllText(file, $"\n.loopcLC00h7{realpoz}:\n");
//        If_and_loop.Parswhile(realcommandto.TrimStart().Substring(2), poz, file, inputFilePath, func, errorcount);
//        List<string> lines = File.ReadAllLines(file).ToList();
//        for (int i = 0; i < lines.Count(); i++)
//        {
//            if (lines[i] == $".loopcLC00h7{realpoz}:")
//            {
//                lines[i] = "";

//            }
//            if (lines[i].TrimStart().Contains("j") && lines[i].TrimStart().Contains($".loopcLC00h7{realpoz}"))
//            {

//                string[] p = lines[i].TrimStart().Split(' ', 2);
//                watconwhilrdo.Add(p[0]);
//            }
//        }

//        watconwhiledo = lines[lines.Count - 2];
//        lines[lines.Count - 1] = "";
//        lines[lines.Count - 2] = "";
//        preorityloop.Add("dowhile");
//        File.WriteAllLines(file, lines);
//        File.AppendAllText(file, $"\n.loopLC00h7{realpoz}:\n");
//        indowhile = true;
//    }
