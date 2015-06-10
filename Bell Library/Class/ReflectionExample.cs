using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MessageBox = System.Windows.Forms.MessageBox;
using System.Reflection;

namespace BellLib.Class
{
    class TestClass
    {
        // Two Fields
        public string s = "문 자 열";
        private static int i = 1;

        // Two Methods
        private static void Methodee1()
        {
            MessageBox.Show("Methodee1 Called.");
        }

        public void Methodee2(int i)
        {
            MessageBox.Show("Methodee2 Called. Value is " + i.ToString());
        }
    }

    public class ReflectionExample
    {
        public static void Example()
        {
            // 일단 인스턴스 하나 만들자구요!
            TestClass tc = new TestClass();

            Type t = typeof(TestClass);

            // TestClass의 필드인 s의 값 구하기. 인스턴스 tc에서!
            string s = (string)t.GetField("s").GetValue(tc);
            MessageBox.Show(s);

            // TestClass에서 정적 필드인 i의 값 구하기. 인스턴스가 없고 정적멤버니까 null!
            int i = (int)t.GetField("i", BindingFlags.NonPublic | BindingFlags.Static).GetValue(null);
            MessageBox.Show(i.ToString());

            // 필드'들' 구하기
            foreach (var a in t.GetFields())
                MessageBox.Show("이름 : " + a.Name + Environment.NewLine +
                                "타입 : " + a.FieldType + Environment.NewLine +
                                "속성 : " + a.ReflectedType);

            // Property는 GetProperty 하면 되니까 생략.

            // Methodee1 호출.
            t.GetMethod("Methodee1", BindingFlags.NonPublic | BindingFlags.Static)
                .Invoke(null, null); // i는 아까 얻은 필드 i값. null은 인스턴스.

            // Methodee2 호출.
            t.GetMethod("Methodee2").Invoke(tc, new object[] { 100 }); // 매개 변수는 오브젝트 배열로 만들어서 전달.
        }
    }
}
