using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BellLib.Class
{
    interface IRegistry
    {
        object GetValue(object defaultValue = null, bool throwException = false);
    }

    public class RegistryReader : IRegistry
    {
        private string key;
        private RegistryKey baseRegKey = Registry.CurrentUser.CreateSubKey("SoftWare").CreateSubKey("BSN");

        #region *** 속성 ***

        /// <summary>
        /// Key 속성입니다.
        /// </summary>
        public string Key
        {
            get { return key; }
            set { key = value; }
        }

        /// <summary>
        /// Base RegistryKey 속성입니다.
        /// </summary>
        public RegistryKey BaseRegKey
        {
            get { return baseRegKey; }
            set { baseRegKey = value; }
        }

        #endregion *** 속성 ***

        #region *** 생성자 ***

        /// <summary>
        /// RegistryReader 클래스를 null인 Key를 사용하여 초기화합니다.
        /// 값을 읽는데에 사용될 수 없습니다.
        /// </summary>
        public RegistryReader()
        {
            key = null;
        }

        /// <summary>
        /// RegistryReader 클래스를 Key를 사용하여 초기화합니다.
        /// </summary>
        /// <param name="_key">Registry Key</param>
        public RegistryReader(string _key)
        {
            if (_key == null)
                throw new ArgumentException();
            else
                key = _key;
        }

        /// <summary>
        /// RegistryReader 클래스를 Key와 기본 RegistryKey를 사용하여 초기화합니다.
        /// </summary>
        /// <param name="_key">Registry Key</param>
        /// <param name="baseKey">Base RegistryKey</param>
        public RegistryReader(string _key, RegistryKey baseKey)
        {
            if (_key == null)
                throw new ArgumentException();
            else
                key = _key;

            baseRegKey = baseKey;
        }

#endregion *** 생성자 ***

        /// <summary>
        /// 인스턴스의 Key를 가지고 Key의 Value를 반환합니다.
        /// </summary>
        /// <param name="defaultValue">값이 없으면 반환 할 값을 나타냅니다.</param>
        /// <param name="throwException">예외가 발생할 시 예외 발생 여부를 나타냅니다.</param>
        /// <returns>Key의 Value를 반환합니다. 값이 없거나 예외 발생 시 defaultValue을 반환합니다.</returns>
        public object GetValue(object defaultValue = null, bool throwException = false)
        {
            RegistryManager rm = new RegistryManager(key);

            object value = rm.GetValue(defaultValue, throwException);

            rm.Dispose();

            return value;
        }
    }

    public class RegistryManager : IRegistry, IDisposable
    {
        bool disposed = false;
        private KeyValuePair<string, object> regPair;
        private RegistryKey baseRegKey = Registry.CurrentUser.CreateSubKey("SoftWare").CreateSubKey("BSN");
        private RegistryValueKind regKind;

        #region  *** 속성 ***

        /// <summary>
        /// 레지스트리 Key, Value값인 KeyValuePair&lt;string, object&gt; 속성입니다.
        /// </summary>
        public KeyValuePair<string, object> RegPair
        {
            get { return regPair; }
            set
            {
                if (value.Key != null)
                    regPair = value;
                else
                    throw new ArgumentNullException("Key", "Key값은 예외일 수 없습니다.");
            }
        }

        /// <summary>
        /// Base RegistryKey 속성입니다.
        /// </summary>
        public RegistryKey BaseRegKey
        {
            get { return baseRegKey; }
            set { baseRegKey = value; }
        }

        /// <summary>
        /// 기록에 사용할 RegistryValueKind 속성입니다.
        /// </summary>
        public RegistryValueKind RegKind
        {
            get { return regKind; }
            set { regKind = value; }
        }

        #endregion  *** 속성 ***

        #region *** 생성자 ***

        /// <summary>
        /// RegistryManager 클래스 초기화합니다. Key와 Value는 null입니다.
        /// </summary>
        public RegistryManager()
        {
            regPair = new KeyValuePair<string, object>(null, null);
        }

        /// <summary>
        /// RegistryManager 클래스를 Key를 사용하여 초기화합니다. Value는 null입니다.
        /// </summary>
        public RegistryManager(string key)
        {
            regPair = new KeyValuePair<string, object>(key, null);
        }

        /// <summary>
        /// RegistryManager 클래스를 Key와 Value를 사용하여 초기화합니다.
        /// </summary>
        /// <param name="key">Registry Key</param>
        /// <param name="value">Registry Value</param>
        /// <param name="_regKind">기록에 사용할 RegistryValueKind 값. 기본값은 String입니다.</param>
        public RegistryManager(string key, object value, RegistryValueKind _regKind = RegistryValueKind.String)
        {
            regPair = new KeyValuePair<string, object>(key, value);
            regKind = _regKind;
        }

        /// <summary>
        /// RegistryManager 클래스를 Key와 Value, 기본 RegistryKey를 사용하여 초기화합니다.
        /// </summary>
        /// <param name="key">Registry Key</param>
        /// <param name="value">Registry Value</param>
        /// <param name="baseKey">Base RegistryKey</param>
        /// <param name="_regKind">기록에 사용할 RegistryValueKind 값. 기본값은 String입니다.</param>
        public RegistryManager(string key, object value, RegistryKey baseKey,
                                                         RegistryValueKind _regKind = RegistryValueKind.String)
        {
            regPair = new KeyValuePair<string, object>(key, value);
            baseRegKey = baseKey;
            regKind = _regKind;
        }

        #endregion *** 생성자 ***

        #region *** 소멸자 ***

        ~RegistryManager()
        {
            baseRegKey.Flush();
            baseRegKey.Close();
        }

        #endregion *** 소멸자 ***

        #region *** IDisposable ***

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                baseRegKey.Flush();
                baseRegKey.Close();
            }

            disposed = true;
        }

        #endregion *** IDisposable ***

        /// <summary>
        /// 인스턴스의 Key와 Value를 가지고 Key에 Value를 대입합니다. 존재하지 않으면 새로 생성합니다.
        /// </summary>
        /// <param name="k">Registry Value의 종류를 나타냅니다.</param>
        /// <param name="throwException">예외가 발생할 시 예외 발생 여부를 나타냅니다.</param>
        /// <returns>성공 여부를 나타냅니다. 성공했으면 true, 그렇지 않으면 false입니다.</returns>
        public bool SetValue(bool throwException = false)
        {
            try
            {
                baseRegKey.SetValue(regPair.Key, regPair.Value, regKind);
            }
            catch (Exception ex)
            {
                if (throwException)
                    throw ex;
                else
                    return false;
            }

            return true;
        }

        /// <summary>
        /// 인스턴스의 Key를 가지고 Key의 Value를 반환합니다.
        /// </summary>
        /// <param name="defaultValue">값이 없으면 반환 할 값을 나타냅니다.</param>
        /// <param name="throwException">예외가 발생할 시 예외 발생 여부를 나타냅니다.</param>
        /// <returns>Key의 Value를 반환합니다. 값이 없거나 예외 발생 시 defaultValue을 반환합니다.</returns>
        public object GetValue(object defaultValue = null, bool throwException = false)
        {
            try
            {
                return baseRegKey.GetValue(regPair.Key, defaultValue);
            }
            catch (Exception ex)
            {
                if (throwException)
                    throw ex;
                else
                    return defaultValue;
            }
        }

        /// <summary>
        /// 인스턴스의 Key에서 지정된 값을 삭제합니다.
        /// </summary>
        /// <param name="throwException">예외가 발생할 시 예외 발생 여부를 나타냅니다.</param>
        /// <returns>성공 여부를 반환합니다. 성공했으면 true, 그렇지 않으면 false입니다.</returns>
        public bool DeleteValue(bool throwException = false)
        {
            try
            {
                baseRegKey.DeleteValue(regPair.Key, true);
            }
            catch (Exception ex)
            {
                if (throwException)
                    throw ex;
                else
                    return false;
            }

            return true;
        }
    }
}
