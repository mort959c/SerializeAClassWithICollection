using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Serialization;

namespace SerializeAClassWithCollection
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnSerializeCollection_Click(object sender, RoutedEventArgs e)
        {
            // The path can ofcause be changed to whatever you want
            SerializeCollection(@"\\data.cv.local\Students\mort959c\Documents\Tilbage på aspit\Project\SavedFiles\Employees.txt");
        }


        /// <summary>
        /// As of now the method writes:
        /// <?xml version="1.0" encoding="utf-8"?>
        ///<ArrayOfEmployee xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
        ///  <Employee>
        ///    <EmpName>John</EmpName>
        ///    <EmpID>100xxx</EmpID>
        /// </Employee>
        ///  <Employee>
        ///    <EmpName>Bent</EmpName>
        ///    <EmpID>200yyy</EmpID>
        ///  </Employee>
        ///  <Employee>
        ///    <EmpName>Grethe</EmpName>
        ///    <EmpID>300zzz</EmpID>
        ///  </Employee>
        ///</ArrayOfEmployee>
        /// </summary>
        /// <param name="path"></param>
        private void SerializeCollection(string path)
        {
            Employees emps = new Employees();
            // Note that only the collection is serialized -- not the collectionName or any other public property of the class
            emps.CollectionName = "Employees";
            Employee John100 = new Employee("John", "100xxx");
            emps.Add(John100);
            Employee Bent200 = new Employee("Bent", "200yyy");
            emps.Add(Bent200);
            Employee Grethe300 = new Employee("Grethe", "300zzz");
            emps.Add(Grethe300);

            XmlSerializer xSer = new XmlSerializer(typeof(Employees));
            TextWriter writer = new StreamWriter(path);
            xSer.Serialize(writer, emps);
        }
    }

    public class Employees : ICollection
    {
        public string CollectionName;
        private ArrayList empArray = new ArrayList();

        public Employee this[int index]
        {
            get { return (Employee)empArray[index]; }
        }

        public int Count
        {
            get { return empArray.Count; }
        }

        public object SyncRoot
        {
            get { return this; }
        }

        public bool IsSynchronized
        {
            get { return false; }
        }

        public void CopyTo(Array array, int index)
        {
            empArray.CopyTo(array, index);
        }

        public IEnumerator GetEnumerator()
        {
            return empArray.GetEnumerator();
        }

        public void Add(Employee newEmployee)
        {
            empArray.Add(newEmployee);
        }
    }

    public class Employee
    {
        public string EmpName;
        public string EmpID;
        public Employee() { }
        public Employee(string empName, string empId)
        {
            this.EmpName = empName;
            this.EmpID = empId;
        }
    }
}
