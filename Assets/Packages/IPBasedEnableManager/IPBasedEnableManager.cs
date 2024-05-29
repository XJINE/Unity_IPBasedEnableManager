using System.Collections.Generic;
using System.Linq;
using System.Net;
using UnityEngine;

public class IPBasedEnableManager : MonoBehaviour
{
    #region Field

    public List<string>        ipAddresses;
    public List<GameObject>    targetObjects;
    public List<MonoBehaviour> targetComponents;

    #endregion Field

    #region Method

    public void EnableTargets()  { Setup(true);  }
    public void DisableTargets() { Setup(false); }

    private void Setup(bool enable)
    {
        foreach (var localAddress in Dns.GetHostEntry("").AddressList)
        {
            foreach (var ipAddress in ipAddresses.Where(ipAddress => Equals(localAddress, IPAddress.Parse(ipAddress))))
            {
                foreach (var targetComponent in targetComponents)
                {
                    targetComponent.enabled = enable;
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