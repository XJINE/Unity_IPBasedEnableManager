using System.Collections.Generic;
using System.Linq;
using System.Net;
using UnityEngine;

namespace IPBasedEnableManagers
{
    public class IPBasedEnableManager : MonoBehaviour
    {
        public enum StartAction
        {
            DoNothing,
            EnableTargets,
            DisableTargets,
        }
    
        #region Field
    
        public StartAction      startAction;
        public List<string>     ipAddresses;
        public List<GameObject> targetObjects;
        public List<Behaviour>  targetBehaviours;
    
        #endregion Field
    
        #region Method
    
        public void Start()
        {
            switch (startAction)
            {
                case StartAction.DisableTargets : DisableTargets(); break;
                case StartAction.EnableTargets  : EnableTargets();  break;
                case StartAction.DoNothing      :                   break;
            }
        }
    
        public void EnableTargets()  { Setup(true);  }
        public void DisableTargets() { Setup(false); }
    
        private void Setup(bool enable)
        {
            foreach (var localAddress in Dns.GetHostEntry("").AddressList)
            {
                foreach (var ipAddress in ipAddresses.Where(ipAddress => Equals(localAddress, IPAddress.Parse(ipAddress))))
                {
                    foreach (var targetBehaviour in targetBehaviours)
                    {
                        targetBehaviour.enabled = enable;
                    }
    
                    foreach (var targetObject in targetObjects)
                    {
                        targetObject.SetActive(enable);
                    }
                }
            }
        }
    
        #endregion Method
    }
}