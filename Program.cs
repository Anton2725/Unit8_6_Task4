using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections;
using System.Runtime.Serialization;

namespace FinalTask
{
    [Serializable]
    class Student
    {
        public string Name { get; set; }
        public string Group { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
    class Program
    {
        const string fileName = @"h:\projects\CDEV-9\temp\Students.dat";
        static void Main(string[] args)
        {
            /*Написать программу-загрузчик данных из бинарного формата в текст.

            На вход программа получает бинарный файл, предположительно, это база данных студентов.

            Свойства сущности Student:
                        Имя — Name(string);
                        Группа — Group(string);
                        Дата рождения — DateOfBirth(DateTime).
            
            Ваша программа должна:
            1. Создать на рабочем столе директорию Students.
            2. Внутри раскидать всех студентов из файла по группам (каждая группа-отдельный текстовый файл), 
               в файле группы студенты перечислены построчно в формате "Имя, дата рождения".
            
            Критерии оценивания
            0 баллов: задача решена неверно или отсутствует доступ к репозиторию.
            2 балла(хорошо): есть недочеты.
            4 балла(отлично): программа работает верно.*/

            var pathDesktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            string pathDirStudents = pathDesktop + @"\Students";
            //Console.WriteLine(path);
            if (!Directory.Exists(pathDirStudents))
            {
                try
                {
                    Directory.CreateDirectory(pathDirStudents);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    return;
                }
            }

            using (var fs = new FileStream(fileName, FileMode.Open))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                Student[] students = (Student[])formatter.Deserialize(fs);

                foreach (var student in students)
                {
                    Console.WriteLine($"Имя: {student.Name} --- Группа: {student.Group} --- Дата рождения: {student.DateOfBirth.ToShortDateString()}");
                    string fileName = pathDirStudents + @"\" + student.Group + ".txt";
                    try
                    {
                        using (StreamWriter sw = new StreamWriter(fileName, true, System.Text.Encoding.Default))
                        {
                            sw.WriteLine($"{student.Name}, {student.DateOfBirth.ToShortDateString()}");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }

            Console.ReadLine();
        }
    }
}
