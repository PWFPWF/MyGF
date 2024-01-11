using System;
using System.IO;
using UnityEngine;
using LitJson;
public class SaveLoad
{
    //存档
    public static void Save_Fun<T>(T obj, string fullName = null) where T : class
    {
        //存档路径 = 没自定路径 项目.数据存储路径/对象名或自定文件名.json ： 使用自定路径
        fullName = (fullName == null) ? $"{Application.persistentDataPath}/{typeof(T)}.json" : fullName;
        string jsonData = JsonMapper.ToJson(obj);
        StreamWriter _pri_Sw = new StreamWriter(fullName);                //写入流路径
        _pri_Sw.Write(jsonData);                //写入该路经文件的内容
        _pri_Sw.Close();                //关闭写入流
    }
    //读档
    public static T Read_Fun<T>(string fullName = null, Func<T> nullDo = null, Func<T> action = null) where T : class
    {
        //存档路径 = 没自定路径 项目.数据存储路径/对象名或自定文件名.json ： 使用自定路径
        fullName = (fullName == null) ? $"{Application.persistentDataPath}/{typeof(T)}.json" : fullName;
        //存在文件
        if (File.Exists(fullName))
        {
            StreamReader _pri_SR = new StreamReader(fullName);                //读取流路径
            string _PriPathJson = _pri_SR.ReadToEnd();                //获取读取到的内容
            _pri_SR.Close();                //关闭读取流
            try
            {
                return JsonMapper.ToObject<T>(_PriPathJson);                //读取到的数据转换成对象
            }
            //转换数据类型失败
            catch (JsonException)
            {

            }
        }
        //没有存档
        else 
        {
         return action?.Invoke();
        }
        return nullDo?.Invoke();      //返回读档失败处理的避空调用
    }
}
