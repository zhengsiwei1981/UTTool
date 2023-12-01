#### 项目说明

    本项目技术栈如下：
    开发工具：Visual Studio 2022 / Jetbrains Rider
    开发语言：C#
    框架：Winform + .Net 7.0
  

#### 背景

    由于工作需要，项目的Unit Case需要达到一定的覆盖率，但是写UT又是一个比较繁琐的工作。
    而且逻辑复杂的业务逻辑，UT会占用大量的时间。
    因此开发一个快速分析代码，并生成UT代码的工具就显得很有必要了

#### 怎么使用

  - 下载源代码，然后使用Visual Studio打开，运行UI项目
  ~~~
    git clone https://github.com/zhengsiwei1981/UTTool.git
  ~~~
  - 设置UTTool.UI项目为启动项目
    ![image](https://github.com/zhengsiwei1981/UTTool/assets/3821091/2544f676-2c22-4c3d-9557-0df67123adb2)
  - 运行项目打开主界面
    ![image](https://github.com/zhengsiwei1981/UTTool/assets/3821091/97c71353-d86c-4672-8f79-78204ed26261)
  - 菜单-文件，或者菜单-文件夹，选择一个目标文件或者文件夹
    
     ![image](https://github.com/zhengsiwei1981/UTTool/assets/3821091/ccdfa80d-f79e-43bf-87db-0be1ab059443)
    

  - 读取DLL文件，生成UT或者生成文件

    ![image](https://github.com/zhengsiwei1981/UTTool/assets/3821091/741f0fe1-d05e-4b53-afae-6661c546335f)
    ![image](https://github.com/zhengsiwei1981/UTTool/assets/3821091/d84895b6-5dfa-4af1-9419-35179d2382d9)

   





