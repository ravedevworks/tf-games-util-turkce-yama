using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Ekstra namespace'ler
using System.IO;
using TFGames_Util_4._0.Cryptography;

namespace TFGames_Util_4._0
{
    class Program
    {
        static void Main(string[] args)
        {
            // >>> EKLENDİ: Terminalden direkt "u" ile isAuto silme
            if (args.Length > 0 && (args[0] == "U" || args[0] == "u"))
            {
                if (File.Exists("isAuto"))
                {
                    try
                    {
                        File.SetAttributes("isAuto", FileAttributes.Normal);
                        File.Delete("isAuto");
                        Console.WriteLine("Otomatik mod kapatıldı.");
                    }
                    catch (IOException ex)
                    {
                        Console.WriteLine("Dosya silinemedi. Başka bir program tarafından kullanılıyor olabilir." + ex.Message);
                    }
                }
                else
                {
                    Console.WriteLine("Otomatik mod zaten aktif değil");
                }
                return; // Program burada direkt kapanacak, tablo açılmaz
            }
            Console.WriteLine("Patrik Nusszer & Storm. - TF Games Yapılandırma Ayarları Kodlayıcısı");
            Console.WriteLine();
            Console.WriteLine("======================== TUŞ KULLANIM TABLOSU ========================");
            Console.WriteLine("+--------+-----------------------------------------------------------+");
            Console.WriteLine("| Tuş    | Açıklama                                                  |");
            Console.WriteLine("+--------+-----------------------------------------------------------+");
            Console.WriteLine("| E      | Dosya şifreleme.                                          |");
            Console.WriteLine("| D      | Dosya şifresi çözme.                                      |");
            Console.WriteLine("| A      | Otomatik modu aç.                                         |");
            Console.WriteLine("| U      | Otomatik modu kapat.                                      |");
            Console.WriteLine("+--------+-----------------------------------------------------------+");
            Console.WriteLine();

            bool hazir = true;

            if (File.Exists("isAuto"))
            {
                string[] dosyalar = Directory.GetFiles(Directory.GetCurrentDirectory());
                for (int i = 0; i < dosyalar.Length; i++)
                {
                    if (dosyalar[i].IndexOf("Decoded") != -1 || dosyalar[i].IndexOf("decoded") != -1 || dosyalar[i].IndexOf("Decrypted") != -1 || dosyalar[i].IndexOf("decrypt") != -1)
                    {
                        Crypter.Encrypt(dosyalar[i], "Coalesced" + ((i == 0) ? "" : i.ToString()) + ((dosyalar[i].IndexOf("Int") != -1 || dosyalar[i].IndexOf("int") != -1) ? ".int" : ".ini"));
                    }
                    if (dosyalar[i].IndexOf("Coalesced") != -1 || dosyalar[i].IndexOf("Encoded") != -1 || dosyalar[i].IndexOf("encoded") != -1 || dosyalar[i].IndexOf("Encrypted") != -1 || dosyalar[i].IndexOf("encrypted") != -1)
                    {
                        Crypter.Decrypt(dosyalar[i], "Decrypted" + ((i == 0) ? "" : i.ToString()) + ".txt");
                    }
                }
            }
            else
            {
                if (args.Length > 0)
                {
                    Console.WriteLine("Argüman modu aktif.");
                    Console.WriteLine();

                    if (args[0] != "D" && args[0] != "d" && args[0] != "E" && args[0] != "e" && args[0] != "A" && args[0] != "a" && args[0] != "U" && args[0] != "u")
                    {
                        Console.WriteLine("İşlem türü verilmedi veya hatalı.");
                        hazir = false;
                    }

                    if (hazir)
                    {
                        if (args[0] == "E" || args[0] == "e")
                        {
                            if (!File.Exists(args[1]))
                            {
                                Console.WriteLine("Girdi dosyası mevcut değil.");
                                hazir = false;
                            }
                            if (string.IsNullOrWhiteSpace(args[2]))
                            {
                                Console.WriteLine("Çıktı dosyası verilmedi.");
                                hazir = false;
                            }

                            if (hazir)
                            {
                                Crypter.Encrypt(args[1], args[2]);
                                Console.WriteLine(Crypter.wasBE ? "Şifreleme tamamlandı. [XBOX/PS3] [Big Endian]" : "Şifreleme tamamlandı. [Windows] [Little Endian]");
                                Crypter.wasBE = false;
                            }
                        }
                        else if (args[0] == "D" || args[0] == "d")
                        {
                            if (!File.Exists(args[1]))
                            {
                                Console.WriteLine("Girdi dosyası mevcut değil.");
                                hazir = false;
                            }
                            if (string.IsNullOrWhiteSpace(args[2]))
                            {
                                Console.WriteLine("Çıktı dosyası verilmedi.");
                                hazir = false;
                            }

                            if (hazir)
                            {
                                Crypter.Decrypt(args[1], args[2]);
                                Console.WriteLine(Crypter.wasBE ? "Şifre çözme tamamlandı. [XBOX/PS3] [Big Endian]" : "Şifre çözme tamamlandı. [Windows] [Little Endian]");
                                Crypter.wasBE = false;
                            }
                        }
                        else if (args[0] == "A" || args[0] == "a")
                        {
                            FileStream fs = new FileStream("isAuto", FileMode.Create);
                            fs.Close();
                        }
                        else if (args[0] == "U" || args[0] == "u")
                        {
                            if (File.Exists("isAuto"))
                            {
                                try
                                {
                                    File.SetAttributes("isAuto", FileAttributes.Normal);
                                    File.Delete("isAuto");
                                    Console.WriteLine("Otomatik mod kapatıldı.");
                                }
                                catch (IOException ex)
                                {
                                    Console.WriteLine("Dosya silinemedi. Başka bir program tarafından kullanılıyor olabilir." + ex.Message);
                                }
                            }
                            else
                            {
                                Console.WriteLine("Otomatik mod zaten aktif değil.");
                            }
                            Console.WriteLine();
                            return;
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Doğrudan mod aktif.");
                    Console.WriteLine();
                    ConsoleKeyInfo cki = new ConsoleKeyInfo('R', ConsoleKey.R, false, false, false);

                    while (cki.KeyChar == 'r' || cki.KeyChar == 'R')
                    {
                        string islem = null;
                        string girdi = null;
                        string cikti = null;
                        hazir = false;

                        while (!hazir)
                        {
                            Console.Write("Lütfen işlemi girin. (E/D/A/U): ");
                            islem = Convert.ToString(Console.ReadKey().KeyChar);
                            Console.WriteLine();
                            Console.WriteLine();
                            if (islem != "D" && islem != "d" && islem != "E" && islem != "e" && islem != "A" && islem != "a" && islem != "U" && islem != "u")
                            {
                                Console.WriteLine("İşlem türü verilmedi veya hatalı.");
                                Console.WriteLine();
                            }
                            else
                            {
                                hazir = true;
                            }
                        }

                        if (islem == "A" || islem == "a")
                        {
                            FileStream fs = new FileStream("isAuto", FileMode.Create);
                            fs.Close();
                            Console.WriteLine("Otomasyon dosyası oluşturuldu.");
                            Console.WriteLine("Otomatik modu kapatmak için 'isAuto' dosyasını silin veya konsolda U tuşunu kullanın.");
                            Console.WriteLine();
                        }
                        else if (islem == "U" || islem == "u")
                        {
                            if (File.Exists("isAuto"))
                            {
                                try
                                {
                                    File.SetAttributes("isAuto", FileAttributes.Normal);
                                    File.Delete("isAuto");
                                    Console.WriteLine("Otomatik mod kapatıldı.");
                                }
                                catch (IOException ex)
                                {
                                    Console.WriteLine("Dosya silinemedi. Başka bir program tarafından kullanılıyor olabilir." + ex.Message);
                                }
                            }
                            else
                            {
                                Console.WriteLine("Otomatik mod zaten aktif değil.");
                            }
                            Console.WriteLine();
                        }
                        else
                        {
                            hazir = false;

                            while (!hazir)
                            {
                                Console.Write("Lütfen girdi dosyasını girin: ");
                                girdi = Console.ReadLine();
                                Console.WriteLine();
                                if (!File.Exists(girdi))
                                {
                                    Console.WriteLine("Girdi dosyası mevcut değil.");
                                    Console.WriteLine();
                                }
                                else
                                {
                                    hazir = true;
                                }
                            }

                            hazir = false;

                            while (!hazir)
                            {
                                Console.Write("Lütfen çıktı dosyasını girin: ");
                                cikti = Console.ReadLine();
                                if (string.IsNullOrWhiteSpace(cikti))
                                {
                                    Console.WriteLine("Çıktı dosyası verilmedi.");
                                    Console.WriteLine();
                                }
                                else
                                {
                                    hazir = true;
                                }
                            }

                            Console.WriteLine();

                            if (islem == "E" || islem == "e")
                            {
                                Crypter.Encrypt(girdi, cikti);
                                Console.WriteLine(Crypter.wasBE ? "Şifreleme tamamlandı [XBOX/PS3] [Big Endian]" : "Şifreleme tamamlandı [Windows] [Little Endian]");
                                Crypter.wasBE = false;
                            }
                            else
                            {
                                Crypter.Decrypt(girdi, cikti);
                                Console.WriteLine(Crypter.wasBE ? "Şifre çözme tamamlandı [XBOX/PS3] [Big Endian]" : "Şifre çözme tamamlandı [Windows] [Little Endian]");
                                Crypter.wasBE = false;
                            }

                            Console.WriteLine();
                            Console.Write("Tekrar düzenlemek için 'r', çıkmak için başka bir tuşa basın.");
                            cki = Console.ReadKey();
                            Console.WriteLine();
                            Console.WriteLine();
                        }
                    }
                }
            }
        }
    }
}