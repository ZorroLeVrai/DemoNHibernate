using NHibernate;

namespace DemoNHibernate
{
    public class StudentHelper
    {
        private ISessionFactory _sessionFactory;

        public StudentHelper(ISessionFactory sessionFactory)
        {
            _sessionFactory = sessionFactory;
        }

        public bool AddStudent(string firstName, string lastName)
        {
            using ISession session = _sessionFactory.OpenSession();
            using ITransaction transaction = session.BeginTransaction();
            try
            {
                var student = new Student
                {
                    FirstName = firstName,
                    LastName = lastName
                };

                session.Save(student);

                transaction.Commit();
                return true;
            }
            catch (Exception)
            {
                if (null != transaction)
                    transaction.Rollback();

                return false;
            }
        }

        public IEnumerable<Student> GetAllStudents()
        {
            using ISession session = _sessionFactory.OpenSession();
            using ITransaction transaction = session.BeginTransaction();

            try
            {
                var students = session.Query<Student>()
                    .ToList();

                transaction.Commit();

                return students;
            }
            catch (Exception)
            {
                if (null != transaction)
                    transaction.Rollback();

                Console.WriteLine("Transaction has been rolled back");

                return Enumerable.Empty<Student>();
            }
        }

        public IEnumerable<Student> SelectStudents(Func<Student, bool> select)
        {
            using ISession session = _sessionFactory.OpenSession();
            using ITransaction transaction = session.BeginTransaction();

            try
            {
                var students = session.Query<Student>()
                    .Where(select)
                    .ToList();

                transaction.Commit();

                return students;
            }
            catch (Exception)
            {
                if (null != transaction)
                    transaction.Rollback();

                Console.WriteLine("Transaction has been rolled back");

                return Enumerable.Empty<Student>();
            }
        }
    }
}