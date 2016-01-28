using BellLib.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BellLib.Class.Analysis
{
    public class Data
    {

        /// <summary>
        /// BDX파일에 저장된 특정 데이터를 로드합니다.
        /// </summary>
        /// <param name="Path">BDX파일 경로</param>
        /// <param name="Name">불러올 데이터 이름</param>
        /// <returns>데이터 이름에 해당하는 데이터 값</returns>
        public static string DataLoad(string Path, string Name)
        {
            string[] DataList;
            try
            {
                DataList = Protection.ReadBDXFile(Path);
            }
            catch
            {
                return null; // 로드실패.
            }

            foreach (string Data in DataList)
            {
                string[] Value = Data.Split('|');
                if (Value[0] == Name)
                    return Value[1];
            }
            return null;
        }

        /// <summary>
        /// 특정 데이터값을 기존 BDX파일내 데이터를 손실시키지 않고 저장합니다.
        /// </summary>
        /// <param name="Path">BDX파일 경로</param>
        /// <param name="Name">저장할 데이터 이름</param>
        /// <param name="Value">저장할 데이터 값</param>
        /// <returns>데이터 저장 성공 여부</returns>
        public static bool DataSave(string Path, string Name, string Value)
        {
            List<string> list = new List<string>();
            bool insert = false;
            try
            {
                foreach (string Data in Protection.ReadBDXFile(Path))
                {
                    string[] temp = Data.Split('|');
                    if (temp[0] == Name)
                    {
                        list.Add(Name + "|" + Value);
                        insert = true;
                    }
                    else
                        list.Add(Data);
                }
            } catch { }

            if (!insert)
                list.Add(Name + "|" + Value);
            try
            {
                Protection.WriteBDXFile(Path, list.ToArray()); // 모든 값 저장
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
