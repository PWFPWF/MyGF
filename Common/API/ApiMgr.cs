/*using System.Collections;
using System.Collections.Generic;
using BestHTTP;
using BestHTTP.Cookies;
using System;
using BestHTTP.JSON.LitJson;

/// <summary>
/// 基于BestHttp的接口调用公共类
/// </summary>
public class ApiMgr : BaseInstance_Cla<ApiMgr_Cla>
{
    private Action<string> _pri_APIAction;//接口回调传递的事件
    public List<Cookie> _Pub_Cookies;//登录后获取的cookies
    public string _Pub_Result;//返回结果

    #region 接口调用
    /// <summary>
    /// 带事件回调的接口调用协程
    /// </summary>
    /// <param name="url">接口地址</param>
    /// <param name="dataDic">传递参数</param>
    /// <param name="action">回调事件</param>
    /// <returns></returns>
    public IEnumerator UseAPI_IEnu(string url, Dictionary<string, object> dataDic, Action<string> action)
    {
        using (HTTPRequest request = new HTTPRequest(new Uri(Constants.URL_CONSTANTS_LOCALHOST + url), HTTPMethods.Post, OnRequestFinished))
        {
            //添加参数
            if (dataDic != null && dataDic.Count > 0)
            {
                foreach (var data in dataDic)
                {
                    request.AddField(data.Key, data.Value.ToString());
                }
            }
            //添加Cookies
            request.Cookies = _Pub_Cookies;
            //传递事件
            _pri_APIAction = action;
            //发送请求
            yield return request.Send();
        }
    }
    /// <summary>
    /// 请求接口回调
    /// </summary>
    /// <param name="request"></param>
    /// <param name="response"></param>
    void OnRequestFinished(HTTPRequest request, HTTPResponse response)
    {
        //解析接口返回的数据，根据接口修改，默认返回全部数据
        //_Pub_Result = GetReturnData(response.DataAsText);
        //回调执行事件，带接口返回参数
        _pri_APIAction?.Invoke(response.DataAsText);
    }
    //解析json中的数据(可以根据自己后台接口的返回值进行重写)
    public virtual string GetReturnData(string jsonString)
    {
        JsonData jsonData = JsonMapper.ToObject(jsonString);
        JsonData jsonReturnData = jsonData["data"];
        JsonData jsonReturnCode = jsonData["code"];
        //接口返回code为200时才返回data中的数据
        return jsonReturnCode.ToString().Equals("200") ? JsonMapper.ToJson(jsonReturnData) : null;
    }
    #endregion
}
*/