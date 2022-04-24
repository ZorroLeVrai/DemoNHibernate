// See https://aka.ms/new-console-template for more information

using DemoNHibernate;
using NHibernate;
using NHibernate.Cfg;

void DisplayStudents(IEnumerable<Student> students)
{
    foreach (Student student in students)
    {
        Console.WriteLine($"{student.FirstName} {student.LastName}");
    }
}

ISessionFactory sessionFactory =
            new Configuration().Configure("DemoNHibernate.dll.config").BuildSessionFactory();

var studentHelper = new StudentHelper(sessionFactory);
studentHelper.AddStudent("Adam", "Toto");
studentHelper.AddStudent("Lise", "Toto");

var allStudents = studentHelper.GetAllStudents();
Console.WriteLine("Display all students");
DisplayStudents(allStudents);

var selectedStudents = studentHelper.SelectStudents(student => student.FirstName == "Adam");
Console.WriteLine("Display selected students");

DisplayStudents(selectedStudents);