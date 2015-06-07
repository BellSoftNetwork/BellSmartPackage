using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BellLib.Class
{
    /// <summary>
    /// 암호화 메서드들의 집합입니다.
    /// </summary>
    class Protection
    {
        public string Base64(string Content, bool Encoding, byte Count = 1)
        {
            byte TC = 1;
            if (Encoding == true)
            {
                for (TC = 1; TC <= Count; TC++)
                {
                    Content = (string)Base64Encode(Content);
                }
            }
            else
            {
                for (TC = 1; TC <= Count; TC++)
                {
                    Content = (string)Base64Decode(Content);
                }
            }

            return Content;
        }
        private object Base64Encode(string Content)
        {
            try
            {
                return Convert.ToBase64String(Encoding.UTF8.GetBytes(Content));
                // 그리고 Enco를 Base64 문자 형태로 인코딩하여 Return(반환)하게 됩니다. 
                //예외 발견 시
            }
            catch // (Exception ex)
            {
                return Content;
                //콘텐츠 반환
            }
        }
        private object Base64Decode(string Content)
        {
            //GoTo 문은 매우 비권장되는 문법 사항

            try
            {
                return Encoding.UTF8.GetString(Convert.FromBase64String(Content));
                //예외 발견 시
            }
            catch // (Exception ex)
            {
                return Content;
                //콘텐츠 반환
            }
        }
    }
}
