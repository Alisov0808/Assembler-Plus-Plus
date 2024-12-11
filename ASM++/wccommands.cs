using System.Security.AccessControl;
using System.Text.RegularExpressions;

namespace Lumin
{
    public unsafe static class wccommands
    {
        static string[] st = { };
        static string st2 = "";
        static public string Parsccom(string command, string file, string inputFilePath, int last, bool lin, bool boot, bool winconsole, int poz, int error, bool wingui)
        {
            try
            {
                if (wingui && !lin && !boot && !winconsole)
                {
                    if (command.TrimStart().StartsWith("entrypoint"))
                    {
                        string[] parts = command.TrimStart().Split(new char[] { ' ' }, 2, StringSplitOptions.RemoveEmptyEntries);
                        File.AppendAllText(file, "\n" + "include 'fasm/include/win32a.inc' \nentry " + parts[1]);
                    }
                    else if (command.TrimStart().StartsWith("wingui.createwindow"))
                    {
                        st = command.TrimStart().Split(' ', 2);
                        st2 = st[1];
                        File.AppendAllText(file, "\nwingui._style equ WS_VISIBLE+WS_OVERLAPPEDWINDOW\r\nsection '.data' data readable writeable\r\n  wingui._class TCHAR 'FASMW32',0\r\n  \r\n  wingui._err_reg TCHAR 'Call to RegisterClassEx failed!',0\r\n  wingui._err_create TCHAR 'Call to CreateWindowEx failed!',0\r\n   wingui.wc WNDCLASS\r\n      wingui.type db 'button',0\nwingui.type3 db 'edit',0\r\nwingui.type2 db 'static',0\n  wingui.msg MSG\r\n   wingui.temp dd ?\r\n   wingui.wih dd 0\r\n  hwnd dd ?                \r\nmacro wingui.createwindow wingui._title,winx,winy,winsx,winsy,windowcolor,style\n   xor ebx,ebx\r\n    invoke GetModuleHandle,ebx\r\n    mov [ wingui.wc.hInstance],eax\r\n    invoke LoadCursor,NULL,IDC_ARROW\r\n    mov [ wingui.wc.hCursor],eax\r\n    invoke LoadIcon,NULL,style\r\n    mov [ wingui.wc.hIcon],eax\r\n    mov [ wingui.wc.lpfnWndProc],WindowProc\r\n    mov [ wingui.wc.lpszClassName],wingui._class\r\n    mov [ wingui.wc.hbrBackground],COLOR_WINDOW+windowcolor\r\n    invoke RegisterClass, wingui.wc\r\n\tmov [wingui.wih],winsx\r\n    test eax,eax\r\n    jz err_reg_class\r\n    invoke CreateWindowEx,ebx,wingui._class, wingui._title,wingui._style,winx,winy,winsx,winsy,NULL,NULL,[ wingui.wc.hInstance],ebx\r\n    test eax,eax\r\n    jz err_win_create\r\n    mov [hwnd],eax\r\n\tmov [wingui.temp],eax\r\n  msg_loop:\r\n    invoke GetMessage, wingui.msg,NULL,0,0\r\n    or eax,eax\r\n    jz end_loop\r\n    invoke TranslateMessage, wingui.msg\r\n    invoke DispatchMessage, wingui.msg\r\n    jmp msg_loop\r\n    jmp end_loop\r\n  end_loop:\r\n    invoke ExitProcess,[ wingui.msg.wParam]\r\n  err_reg_class:\r\n    invoke MessageBox,NULL,wingui._err_reg,wingui._title,MB_ICONERROR+MB_OK\r\n    jmp end_loop\r\n  err_win_create:\r\n    invoke MessageBox,NULL,wingui._err_create, wingui._title,MB_ICONERROR+MB_OK\r\n    jmp end_loop\r\n  proc WindowProc uses ebx esi edi,hwnd,wmsg,wingui.index,lparam\r\n    cmp [wmsg],WM_DESTROY\r\n    je .wmdestroy\r\n  cmp [wmsg],WM_COMMAND\n je .wmcommand\n  cmp [wmsg],WM_CREATE\r\n    je .wmcreate\r\n  .defwndproc:\r\n    invoke DefWindowProc,[hwnd],[wmsg],[wingui.index],[lparam]\r\n    jmp finish\r\n\n;Import state is not state");
                    }
                    else if (command.TrimStart().StartsWith("section"))
                    {
                        File.AppendAllText(file, "\n" + command.TrimStart());
                    }
                    else if (command.TrimStart().StartsWith("callwinapic"))
                    {
                        int ind = command.TrimStart().Substring(12).TrimStart().IndexOf(' ');
                        char[] a = command.TrimStart().Substring(12).TrimStart().ToCharArray();
                        a[ind] = ',';
                        command = new String(a);
                        string[] parts = command.TrimStart().Split(',');
                        int buffer = 0;

                        for (int i = 1; i < parts.Length; i++)
                        {
                            if (!int.TryParse(parts[i].Trim(), out buffer) && parts[i].Trim() != "''" && parts[i].Trim() != "\"\"" && !parts[i].Trim().StartsWith("'") && !parts[i].Trim().StartsWith("\""))
                            { parts[i] = $"[{parts[i]}]"; }

                        }
                        string g = string.Join(",", parts);
                        File.AppendAllText(file, "\nicnvoke " + g.TrimStart());
                    }
                    else if (command.TrimStart().StartsWith("callwinapi"))
                    {
                        int ind = command.TrimStart().Substring(11).TrimStart().IndexOf(' ');
                        char[] a = command.TrimStart().Substring(11).TrimStart().ToCharArray();
                        a[ind] = ',';
                        command = new String(a);
                        string[] parts = command.TrimStart().Split(',');
                        int buffer = 0;

                        for (int i = 1; i < parts.Length; i++)
                        {
                            if (!int.TryParse(parts[i].Trim(), out buffer) && parts[i].Trim() != "''" && parts[i].Trim() != "\"\"" && !parts[i].Trim().StartsWith("'") && !parts[i].Trim().StartsWith("\""))
                            { parts[i] = $"[{parts[i]}]"; }

                        }
                        string g = string.Join(",", parts);
                        File.AppendAllText(file, "\ninvoke " + g.TrimStart());
                        // Console.WriteLine(command);
                    }
                    else if (command.TrimStart().StartsWith("exit"))
                    {
                        File.AppendAllText(file, "\n" + "invoke ExitProcess, 0 ");
                    }
                   
                    if (command.TrimStart().StartsWith("strcmp"))
                    {
                        string commandBody = command.Replace("strcmp", " ");
                        string[] parts = commandBody.Split(new string[] { " then " }, StringSplitOptions.None);


                        string functionName = parts[1].Trim();
                        string arguments = parts[0].Trim();

                        string[] args = arguments.Split(',', StringSplitOptions.RemoveEmptyEntries);

                        string string1 = args[0].Trim();

                        string assemblyCode;

                        assemblyCode = $"\n   invoke lstrcmpA, [{args[0]}],[{args[1]}]\r\n    cmp eax, 0\r\n    je {parts[1]}";
                        File.AppendAllText(file, assemblyCode);

                        last++;
                    }
                    if (command.TrimStart().StartsWith("system"))
                    {
                        string[] parts = command.TrimStart().Split(new char[] { ' ' }, 2, StringSplitOptions.RemoveEmptyEntries);
                        string pattern = @"\d+";
                        bool containsNumbers = false;
                        try
                        {
                            containsNumbers = Regex.IsMatch(parts[1], pattern);
                        }
                        catch { }
                        File.AppendAllText(file, "\n" + $"invoke system,{parts[1]}  ");
                    }
                    if (command.TrimStart().StartsWith("sleep"))
                    {
                        string[] parts = command.TrimStart().Split(new char[] { ' ' }, 2, StringSplitOptions.RemoveEmptyEntries);
                        string pattern = @"\d+";
                        bool containsNumbers = false;
                        try
                        {
                            containsNumbers = Regex.IsMatch(parts[1], pattern);
                        }
                        catch { }
                        if (char.IsLetter(parts[1][0]) && parts[1].Contains("'") == false)
                        {
                            File.AppendAllText(file, "\n" + $"  invoke Sleep,[{parts[1]}]   ");
                        }
                        else { File.AppendAllText(file, "\n" + $"invoke Sleep,{parts[1]} "); }
                    }
                }
            }
            catch (Exception)
            {

                Console.WriteLine("Error: Invalid wingui command syntax! On line: " + (poz + 1)); error++;
            }
            try
            {
                if (boot)
                {
                    if (command.Trim().StartsWith("beep"))
                    {
                        File.AppendAllText(file, "\n mov ah, 0x0E \r\n    mov al, 0x07\r\n    int 0x10 ");
                    }
                    else if (command.TrimStart().StartsWith("setcurpoz "))
                    {
                        string[] parts = command.TrimStart().Split(new char[] { ' ' }, 2, StringSplitOptions.RemoveEmptyEntries);
                        string[] t = parts[1].Split(new char[] { ',' }, 2, StringSplitOptions.RemoveEmptyEntries);
                        string pattern = @"\d+";
                        bool containsNumbers3 = Regex.IsMatch(t[1], pattern);
                        bool containsNumbers2 = Regex.IsMatch(t[0], pattern);
                        if (!char.IsLetter(t[0][0]) && !char.IsLetter(t[1][0]))
                        {
                            File.AppendAllText(file, "\n" + $" mov ah, 0x02     \r\n    mov bh, 0x00    \r\n    mov dh, {t[1]}      \r\n    mov dl, {t[1]}     \r\n    int 0x10");
                        }
                        else if (char.IsLetter(t[0][0]) && char.IsLetter(t[1][0]))
                        {
                            File.AppendAllText(file, "\n" + $" mov ah, 0x02     \r\n    mov bh, 0x00    \r\n    mov dh, [{t[1]}]      \r\n    mov dl, [{t[1]}]     \r\n    int 0x10");

                            // File.AppendAllText(file, "\n" + $"  mov ah, 0x02\r\nmov bh, 0x00\r\nmov al, byte [{t[0]}]\nmov dh,al \r\nmov al, byte[{t[1]}]\nmov dl,al\r\nint 0x10");
                        }
                        else if (!char.IsLetter(t[0][0]) && char.IsLetter(t[1][0]))
                        {
                            File.AppendAllText(file, "\n" + $" mov ah, 0x02     \r\n    mov bh, 0x00    \r\n    mov dh, {t[1]}x0      \r\n    mov dl, [{t[1]}]x0     \r\n    int 0x10");

                            // File.AppendAllText(file, "\n" + $" mov ah, 0x02\r\nmov bh, 0x00\r\nmov al, byte {t[0]}\nmov dh,al \r\nmov al, byte[{t[1]}]\nmov dl,al\r\nint 0x10");
                        }
                        else if (char.IsLetter(t[0][0]) && !char.IsLetter(t[1][0]))
                        {
                            File.AppendAllText(file, "\n" + $" mov ah, 0x02     \r\n    mov bh, 0x00    \r\n    mov dh, [{t[1]}]x0      \r\n    mov dl, {t[1]}x0     \r\n    int 0x10");

                            //File.AppendAllText(file, "\n" + $"  mov ah, 0x02\r\nmov bh, 0x00\r\nmov al,byte [{t[0]}]\nmov dh,al \r\nmov al, byte{t[1]}\nmov dl,al\r\nint 0x10");
                        }
                    }
                    else if (command.TrimStart().StartsWith("setcursize "))
                    {
                        string[] parts = command.TrimStart().Split(new char[] { ' ' }, 2, StringSplitOptions.RemoveEmptyEntries);
                        string[] t = parts[1].Split(new char[] { ',' }, 2, StringSplitOptions.RemoveEmptyEntries);
                        string pattern = @"\d+";
                        bool containsNumbers3 = Regex.IsMatch(t[1], pattern);
                        bool containsNumbers2 = Regex.IsMatch(t[0], pattern);
                        if (!char.IsLetter(t[0][0]) && !char.IsLetter(t[1][0]))
                        {
                            File.AppendAllText(file, "\n" + $" mov ah, 0x01\r\nmov ch, {t[0]}\r\nmov cl, {t[1]}\r\nint 0x10");
                        }
                        else if (char.IsLetter(t[0][0]) && char.IsLetter(t[1][0]))
                        {
                            File.AppendAllText(file, "\n" + $"  mov ah, 0x01\r\nmov ch, [{t[0]}]\r\nmov cl, [{t[1]}]\r\nint 0x10");
                        }
                        else if (!char.IsLetter(t[0][0]) && char.IsLetter(t[1][0]))
                        {
                            File.AppendAllText(file, "\n" + $"  mov ah, 0x01\r\nmov ch, {t[0]}\r\nmov cl, [{t[1]}]\r\nint 0x10");
                        }
                        else if (char.IsLetter(t[0][0]) && !char.IsLetter(t[1][0]))
                        {
                            File.AppendAllText(file, "\n" + $"  mov ah, 0x01\r\nmov ch, [{t[0]}]\r\nmov cl, {t[1]}\r\nint 0x10");
                        }
                    }
                    else if (command.TrimStart().StartsWith("textcolor "))
                    {
                        string[] parts = command.TrimStart().Split(new char[] { ' ' }, 2, StringSplitOptions.RemoveEmptyEntries);
                        string pattern = @"\d+";
                        bool containsNumbers = false;
                        try
                        {
                            containsNumbers = Regex.IsMatch(parts[1], pattern);
                        }
                        catch { }
                        if (!containsNumbers && parts[1].Contains("'") == false)
                        {
                            File.AppendAllText(file, "\n" + $" mov bl,[{parts[1]}]  \r\n    int 0x10    ");
                        }
                        else { File.AppendAllText(file, "\n" + $"  mov bl,{parts[1]}  \r\n    int 0x10  "); }
                    }
                    else if (command.TrimStart().StartsWith("input "))
                    {
                        string[] parts = command.TrimStart().Split(new char[] { ' ' }, 2, StringSplitOptions.RemoveEmptyEntries);
                        string[] t = parts[1].Split(new char[] { ',' }, 2, StringSplitOptions.RemoveEmptyEntries);
                        File.AppendAllText(file, "\n" + $"mov si,0\n Command0{last}: \r\n    mov ah,10h\r\n        int 16h\r\n        cmp ah, 0Eh    \r\n        jz Delete_symbol0{last}\r\n        cmp al, 0Dh\r\n        jz {t[1]}\r\n        mov [{t[0]}+si],al\r\n        inc si\r\n        mov ah,09h\r\n               mov cx,1\r\n        int 10h\r\n   add dl,1\r\n  mov ah,2h\r\n        xor bh,bh\r\n        int 10h \r\n    jmp Command0{last} \nDelete_symbol0{last}:\r\n    cmp dl,0\r\n    jz Command0{last} \r\n    sub dl,1     \r\n    mov ah,2h\r\n        xor bh,bh\r\n        int 10h \r\n    mov al,20h     \r\n    mov [{t[0]} + si],al \r\n    mov ah,09h\r\n            mov cx,1\r\n        int 10h\r\n        dec si       \r\n    jmp Command0{last}        ");
                        last++;
                        last++;
                    }
                    else if (command.TrimStart().StartsWith("print "))
                    {
                        bool chec = false;
                        string[] parts = command.TrimStart().Split(new char[] { ' ' }, 2, StringSplitOptions.RemoveEmptyEntries);
                        if (parts[1].Contains("'"))
                        {
                            parts[1] = parts[1].Replace("'", "");
                            parts[1] = parts[1].Replace("'", "");
                            parts[1] = parts[1].Replace("\"", "");
                            parts[1] = parts[1].Replace("\"", "");

                        }
                        else if (parts[1].Contains("\""))
                        {
                            parts[1] = parts[1].Replace("\"", "\"");
                            parts[1] = parts[1].Replace("\"", "\"");
                        }
                        else { chec = true; }
                        for (int i = 0; i < parts[1].Length; i++)
                        {
                            if (chec)
                            {
                                string[] t = parts[1].Split(',', 2);
                                if (char.IsLetter(t[1][0]))
                                {
                                    File.AppendAllText(file, "\n" + $" mov bp,[{t[0]}]    \r\n   mov cx,[{t[1]}]\r\n        mov ax,1301h      \n       int 10h");
                                }
                                else { File.AppendAllText(file, "\n" + $" mov bp,[{t[0]}]    \r\n   mov cx,{t[1]}\r\n        mov ax,1301h     \n int 10h"); }
                                break;
                            }
                            else
                            {
                                if (parts[1][i] == '\\' && parts[1][i + 1] == 'n') { File.AppendAllText(file, "\n" + $"mov al, 0x0D \r\nint 0x10     \r\nmov al, 0x0A \r\nint 0x10 "); i++; }
                                else
                                {
                                    File.AppendAllText(file, "\n" + $"mov ah, 0x0E \r\nmov al, '{parts[1][i]}'  \r\nint 0x10  ");
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {

                Console.WriteLine("Error: Invalid boot command syntax! On line: " + (poz + 1)); error++;
            }

            try
            {
                if (lin)
                {
                    if (command.TrimStart().StartsWith("syscall"))
                    {
                        File.AppendAllText(file, "\nsyscall");
                        //string[] parts = command.TrimStart().Split(new char[] { ' ' }, 2, StringSplitOptions.RemoveEmptyEntries);
                        //string[] r2d2 = parts[1].Split(',', 2);
                        //if (char.IsLetter(r2d2[1][0]) && r2d2[1].Contains("'") == false&&char.IsLetter(r2d2[0][0]))
                        //{
                        //    File.AppendAllText(file, "\n" + $"mov eax, [{r2d2[0]}]\r\n    mov ebx, [{r2d2[1]}]\r\n    int 0x80 ");
                        //}
                        //else if (!char.IsLetter(r2d2[1][0]) && r2d2[1].Contains("'") == false && char.IsLetter(r2d2[0][0]))
                        //{
                        //    File.AppendAllText(file, "\n" + $"mov eax, [{r2d2[0]}]\r\n    mov ebx, {r2d2[1]}\r\n    int 0x80 ");
                        //}
                        //else if (char.IsLetter(r2d2[1][0]) && r2d2[1].Contains("'") == false && !char.IsLetter(r2d2[0][0]))
                        //{
                        //    File.AppendAllText(file, "\n" + $"mov eax, {r2d2[0]}\r\n    mov ebx, [{r2d2[1]}]\r\n    int 0x80 ");
                        //}
                    }
                    if (command.TrimStart().StartsWith("sleep "))
                    {
                        string[] partsed = command.TrimStart().Split(new char[] { ' ' }, 2, StringSplitOptions.RemoveEmptyEntries);
                        
                       
                        if (char.IsLetter(partsed[1][0]) && partsed[1].Contains("'") == false)
                        {
                            File.AppendAllText(file, "\n" + $"stdcall sleep,[{partsed[1]}] ");
                        }
                        else { File.AppendAllText(file, "\n" + $"stdcall sleep,{partsed[1]}"); }
                    }
                    if (command.TrimStart().StartsWith("entrypoint" ))
                    {
                        string[] parts = command.TrimStart().Split(new char[] { ' ' }, 2, StringSplitOptions.RemoveEmptyEntries);
                        File.AppendAllText(file, "\n" + "entry " + parts[1] + "\n");
                    }
                    if (command.TrimStart().StartsWith("print "))
                    {
                        string[] parts = command.TrimStart().Split(new char[] { ' ' }, 2, StringSplitOptions.RemoveEmptyEntries);
                      
                        if (char.IsLetter(parts[1][0]))
                        {

                            File.AppendAllText(file, "\n" + $"\nstdcall strlen,[{parts[1]}]\nmov edx,eax\nmov eax, 4 \r\n    mov ebx, 1   \r\n    mov ecx,[{parts[1]}] \r\n    int 0x80 ");
                        }
                        else
                        {
                            File.AppendAllText(file, "\n" + $"\nstdcall strlen,[{parts[1]}]\nmov edx,eax\nmov eax, 4  \r\n    mov ebx, 1   \r\n    mov ecx,[{parts[1]}] \r\n    int 0x80 ");

                        }
                    }
                    if (command.TrimStart().StartsWith("section "))
                    {
                        File.AppendAllText(file, "\n" + command.TrimStart().Replace("section", "segment"));
                    }
                    if (command.TrimStart().StartsWith("exit"))
                    {
                        string[] partsed = command.TrimStart().Split(new char[] { ' ' }, 2, StringSplitOptions.RemoveEmptyEntries);


                        if (char.IsLetter(partsed[1][0]) && partsed[1].Contains("'") == false)
                        {
                            File.AppendAllText(file, "\n" + $"stdcall exit,[{partsed[1]}] ");
                        }
                        else { File.AppendAllText(file, "\n" + $"stdcall exit,{partsed[1]}"); }
                    }
                    if (command.TrimStart().StartsWith("waitforkeyexit "))
                    {
                        string[] partsed = command.TrimStart().Split(new char[] { ' ' }, 2, StringSplitOptions.RemoveEmptyEntries);


                        if (char.IsLetter(partsed[1][0]) && partsed[1].Contains("'") == false)
                        {
                            File.AppendAllText(file, "\n" + $"stdcall waitforkeyexit,[{partsed[1]}] ");
                        }
                        else { File.AppendAllText(file, "\n" + $"stdcall waitforkeyexit,{partsed[1]}"); }
                    }
                    if (command.TrimStart().StartsWith("strcmp "))
                    {
                        string commandBody = command.Replace("strcmp", " ");
                        string[] parts = commandBody.Split(new string[] { " then " }, StringSplitOptions.None);


                        string functionName = parts[1].Trim();
                        string arguments = parts[0].Trim();

                        string[] args = arguments.Split(',', 2, StringSplitOptions.RemoveEmptyEntries);
                     
                        string string1 = args[0].Trim();

                        string assemblyCode;
                        if (!Char.IsLetter(args[1][0]))
                        {
                            assemblyCode = $"\nstdcall strlen,[{args[1]}]\n  lea esi,[[{args[0]}]]\r\n        lea edi,[[{args[1]}]]\r\n        mov ecx,eax     \r\n        repe cmpsb  \r\n        je {parts[1]}  ";
                        }
                        else { assemblyCode = $"\nstdcall strlen,[{args[1]}]\n  lea esi,[[{args[0]}]]\r\n        lea edi,[[{args[1]}]]\r\n        mov ecx,eax     \r\n        repe cmpsb  \r\n        je {parts[1]}  "; }
                        File.AppendAllText(file, assemblyCode);

                        last++;
                    }
                    if (command.TrimStart().StartsWith("input "))
                    {
                        string[] partsed = command.TrimStart().Split(new char[] { ' ' }, 2, StringSplitOptions.RemoveEmptyEntries);
                        if (char.IsLetter(partsed[1][0]) && partsed[1].Contains("'") == false)
                        {
                            File.AppendAllText(file, "\n" + $"stdcall input,[{partsed[1]}] ");
                        }
                        else { File.AppendAllText(file, "\n" + $"stdcall input,{partsed[1]}"); }
                    }

                    if (command.TrimStart().StartsWith("clearscreen"))
                    {
                        File.AppendAllText(file, " \n  xor ebx, ebx\r\n    push ebx  \r\n    push argv00hCK  \r\n    push cmd00hCl0  \r\n    mov ecx, esp  \r\n    xor ebx, ebx  \r\n    mov eax, 11  \r\n    int 0x80  ");
                    }

                }
            }
            catch (Exception)
            {

                Console.WriteLine("Error: Invalid linconsole command syntax! On line: " + (poz + 1)); error++;
            }
            try
            {
                if (!lin && !boot && winconsole)
                {

                    if (command.TrimStart().StartsWith("entrypoint"))
                    {
                        string[] parts = command.TrimStart().Split(new char[] { ' ' }, 2, StringSplitOptions.RemoveEmptyEntries);
                        File.AppendAllText(file, "\n" + "include 'fasm/include/win32a.inc' \nentry " + parts[1]);
                    }
                    else if (command.TrimStart().StartsWith("section"))
                    {
                        File.AppendAllText(file, "\n" + command.TrimStart());
                    }
                    else if (command.TrimStart().StartsWith("callwinapic"))
                    {
                        int ind = command.TrimStart().Substring(12).TrimStart().IndexOf(' ');
                        char[] a = command.TrimStart().Substring(12).TrimStart().ToCharArray();
                        a[ind] = ',';
                        command = new String(a);
                        string[] parts = command.TrimStart().Split(',');
                        int buffer = 0;

                        for (int i = 1; i < parts.Length; i++)
                        {
                            if (!int.TryParse(parts[i].Trim(), out buffer) && parts[i].Trim() != "''" && parts[i].Trim() != "\"\"" && !parts[i].Trim().StartsWith("'") && !parts[i].Trim().StartsWith("\""))
                            { parts[i] = $"[{parts[i]}]"; }

                        }
                        string g = string.Join(",", parts);
                        File.AppendAllText(file, "\nicnvoke " + g.TrimStart());
                    }
                    else if (command.TrimStart().StartsWith("callwinapi"))
                    {
                        int ind = command.TrimStart().Substring(11).TrimStart().IndexOf(' ');
                        char[] a = command.TrimStart().Substring(11).TrimStart().ToCharArray();
                        a[ind] = ',';
                        command = new String(a);
                        string[] parts = command.TrimStart().Split(',');
                        int buffer = 0;

                        for (int i = 1; i < parts.Length; i++)
                        {
                            if (!int.TryParse(parts[i].Trim(), out buffer) && parts[i].Trim() != "''" && parts[i].Trim() != "\"\"" && !parts[i].Trim().StartsWith("'") && !parts[i].Trim().StartsWith("\""))
                            { parts[i] = $"[{parts[i]}]"; }

                        }
                        string g = string.Join(",", parts);
                        File.AppendAllText(file, "\ninvoke " + g.TrimStart());
                        // Console.WriteLine(command);
                    }
                    else if (command.TrimStart().StartsWith("exit"))
                    {
                        File.AppendAllText(file, "\n" + "invoke ExitProcess, 0 ");
                    }
                    if (command.TrimStart().StartsWith("allocatem"))
                    {
                        string[] parts = command.TrimStart().Split(new char[] { ' ' }, 2, StringSplitOptions.RemoveEmptyEntries);
                        if (char.IsLetter(parts[1][0]))
                        {
                            File.AppendAllText(file, "\n" + $"invoke malloc,[{parts[1]}]");
                        }
                        else
                        {
                            File.AppendAllText(file, "\n" + $"invoke malloc,{parts[1]}");
                        }
                    }
                    if (command.TrimStart().StartsWith("freem"))
                    {
                        string[] parts = command.TrimStart().Split(new char[] { ' ' }, 2, StringSplitOptions.RemoveEmptyEntries);
                        if (char.IsLetter(parts[1][0]))
                        {
                            File.AppendAllText(file, "\n" + $"invoke free,[{parts[1]}]");
                        }
                        else
                        {
                            File.AppendAllText(file, "\n" + $"invoke free,{parts[1]}");
                        }
                    }
                    if (command.TrimStart().StartsWith("print"))
                    {



                        string[] parts = command.TrimStart().Split(new char[] { ' ' }, 2, StringSplitOptions.RemoveEmptyEntries);
                        File.AppendAllText(file, "\n" + $"\ninvoke printf,[{parts[1]}]");

                    }
                    if (command.TrimStart().StartsWith("waitforkeyexit"))
                    {
                        File.AppendAllText(file, "\n" + " invoke getch\r\n  \r\n  invoke ExitProcess, 0        ");
                    }
                    if (command.TrimStart().StartsWith("setencoding"))
                    {
                        string[] parts = command.TrimStart().Split(new char[] { ' ' }, 2, StringSplitOptions.RemoveEmptyEntries);
                        File.AppendAllText(file, "\n" + $" invoke SetConsoleOutputCP,{parts[1]}\ninvoke SetConsoleCP,{parts[1]}");
                    }
                    if (command.TrimStart().StartsWith("strcmp"))
                    {
                        string commandBody = command.Replace("strcmp", " ");
                        string[] parts = commandBody.Split(new string[] { " then " }, StringSplitOptions.None);


                        string functionName = parts[1].Trim();
                        string arguments = parts[0].Trim();

                        string[] args = arguments.Split(',', StringSplitOptions.RemoveEmptyEntries);

                        string string1 = args[0].Trim();

                        string assemblyCode;

                        assemblyCode = $"\n   invoke lstrcmpA, [{args[0]}],[{args[1]}]\r\n    cmp eax, 0\r\n    je {parts[1]}";
                        File.AppendAllText(file, assemblyCode);

                        last++;
                    }
                    if (command.TrimStart().StartsWith("system"))
                    {
                        string[] parts = command.TrimStart().Split(new char[] { ' ' }, 2, StringSplitOptions.RemoveEmptyEntries);
                        string pattern = @"\d+";
                        bool containsNumbers = false;
                        try
                        {
                            containsNumbers = Regex.IsMatch(parts[1], pattern);
                        }
                        catch { }
                        File.AppendAllText(file, "\n" + $"invoke system,{parts[1]}  ");
                    }
                    if (command.TrimStart().StartsWith("sleep"))
                    {
                        string[] parts = command.TrimStart().Split(new char[] { ' ' }, 2, StringSplitOptions.RemoveEmptyEntries);
                        string pattern = @"\d+";
                        bool containsNumbers = false;
                        try
                        {
                            containsNumbers = Regex.IsMatch(parts[1], pattern);
                        }
                        catch { }
                        if (char.IsLetter(parts[1][0]) && parts[1].Contains("'") == false)
                        {
                            File.AppendAllText(file, "\n" + $"  invoke Sleep,[{parts[1]}]   ");
                        }
                        else { File.AppendAllText(file, "\n" + $"invoke Sleep,{parts[1]} "); }
                    }
                    if (command.TrimStart().StartsWith("input")) 
                    {
                        string a = command.TrimStart().Substring(6);
                        File.AppendAllText(file,$"\ncinvoke scanf,formatString00, {a}");
                    }
                    if (command.TrimStart().StartsWith("clearscreen"))
                    {
                        File.AppendAllText(file, "\ninvoke system,cls000");
                    }
                }
            }
            catch (Exception)
            {

                Console.WriteLine("Error: Invalid winconsole command syntax! On line: " + (poz + 1)); error++;
            }
            try
            {
                if (!lin && !boot && !winconsole)
                {
                    if (command.TrimStart().StartsWith("print"))
                    {
                        bool chec = false;
                        string[] parts = command.TrimStart().Split(new char[] { ' ' }, 2, StringSplitOptions.RemoveEmptyEntries);
                        if (parts[1].Contains("'"))
                        {
                            parts[1] = parts[1].Replace("'", "");
                            parts[1] = parts[1].Replace("'", "");

                        }
                        else if (parts[1].Contains("\""))
                        {
                            parts[1] = parts[1].Replace("\"", "\"");
                            parts[1] = parts[1].Replace("\"", "\"");
                        }
                        else { chec = true; }
                        for (int i = 0; i < parts[1].Length; i++)
                        {
                            if (chec)
                            {
                                string[] t = parts[1].Split(',', 2);
                                if (char.IsLetter(t[1][0]))
                                {
                                    File.AppendAllText(file, "\n" + $" mov bp,[{t[0]}]    \r\n   mov cx,[{t[1]}]\r\n        mov ax,1301h      \r\n        int 10h");
                                }
                                else { File.AppendAllText(file, "\n" + $" mov bp,[{t[0]}]    \r\n   mov cx,{t[1]}\r\n        mov ax,1301h      \r\n        int 10h"); }
                                break;
                            }
                            else
                            {
                                if (parts[1][i] == '\\' && parts[1][i + 1] == 'n') { File.AppendAllText(file, "\n" + $"mov al, 0x0D \r\nint 0x10     \r\nmov al, 0x0A \r\nint 0x10 "); i++; }
                                else
                                {
                                    File.AppendAllText(file, "\n" + $"mov ah, 0x0E \r\nmov al, '{parts[1][i]}'  \r\nint 0x10  ");
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {

                Console.WriteLine("Error: Invalid nonos command syntax! On line: " + (poz + 1)); error++;
            }
            return st2;
        }
    }
}
