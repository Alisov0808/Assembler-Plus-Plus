using System.Diagnostics;
using System.IO;
using System.Text;

namespace Lumin
{
    public static class consoleman
    {
        public static string Parscon()
        {
            Console.WriteLine("Введите help для списка команд, comp [название файла] для компиляции.");
            string currentDirectory = Directory.GetCurrentDirectory().ToString();
            string temp = Directory.GetCurrentDirectory().ToString();
            List<string> list = new List<string>();
            
         //   Console.WriteLine(File.ReadAllText("i.bin"));
            try
            {


                while (true)
                {
                    Console.Write(currentDirectory + ">>");
                    string a = Console.ReadLine();
                    if (a.StartsWith("comp "))
                    {
                        return a.Substring(5).TrimStart();
                    }
                    if (a == "help")
                    {
                        Console.WriteLine("\n----------\ncomp [название файла] - для компиляции\ncls - для очистки экрана\ndir - для просмота файлов и папок в текущей дериктории\ncd [директория] - сменить директорию\ncodecnsl - открывает возможность писать код в консоли\nexitcnsl - codecnsl - закрывает возможность писать код в консоли и запускает программу\n----------\n");
                    }
                    if (a == "cls")
                    {
                        Console.Clear();
                    }
                    if (a == "codecnsl")
                    {
                        while (true)
                        {
                            string u = Console.ReadLine();
                            if (u=="exitcnsl") 
                            {
                                Random random = new Random();
                                int g = random.Next(0, 10000001);
                                string h = temp + $@"\fasm\temp{g}.app";

                                if (!Directory.Exists(temp + @"\fasm"))
                                {
                                    Directory.CreateDirectory(temp + @"\fasm");
                                }

                                Console.WriteLine($"Создание файла: {h}");

                                using (StreamWriter writer = new StreamWriter(h, false, System.Text.Encoding.Default))
                                {
                                  
                                        foreach (var item in list)
                                        {
                                            writer.WriteLine(item);
                                        }
                                    
                                }

                                try
                                {

                             
                                Process process2 = new Process();
                                process2.StartInfo.FileName = "lumin.exe";
                                process2.StartInfo.Arguments = h;
                                process2.StartInfo.UseShellExecute = false;
                                process2.StartInfo.RedirectStandardInput = true;
                                Console.WriteLine(process2.StartInfo.Arguments);
                                process2.Start();
                                process2.WaitForExit();
                                Process process3 = new Process();
                                process3.StartInfo.FileName = h.Replace(".app",".exe");
                                process3.StartInfo.UseShellExecute = false;
                                process3.Start();
                                process3.WaitForExit();
                                File.Delete(h);
                                File.Delete(h.Replace(".app",".asm"));
                                File.Delete(h.Replace(".app", ".exe"));
                                list.Clear(); 
                                }
                                catch (Exception)
                                {

                                }
                                break;
                                
                            }
                           list.Add(u);
                        }
                    }
                    if (a == "dir")
                    {

                        Console.WriteLine($"----{currentDirectory}----");
                        Console.WriteLine();
                        Console.ForegroundColor = ConsoleColor.Blue;
                        foreach (var dir in Directory.GetDirectories(currentDirectory))
                        {
                            Console.WriteLine($"Папка: {Path.GetFileName(dir)}");
                        }
                        Console.ForegroundColor = ConsoleColor.Red;
                        foreach (var file in Directory.GetFiles(currentDirectory))
                        {
                            Console.WriteLine($"Файл: {Path.GetFileName(file)}");
                        }
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine();
                    }
                    if (a.StartsWith("cd "))
                    {
                        currentDirectory = a.Substring(3).TrimStart();
                    }
                }

            }
            catch (Exception)
            {
                return "";

            }

        }
    }
}
