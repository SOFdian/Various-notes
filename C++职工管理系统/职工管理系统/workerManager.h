#pragma once//防止头文件重复包含
#include<iostream>
#include<string>
#include"worker.h"
#include<fstream>
#define FILE "empFile.txt"
using namespace std;

class WorkManager {
public:
	//在头文件中声明，在cpp中实现
	WorkManager();//构造
	void showMenu();//显示主页面
	int Exit();//退出程序
	void addWorker();//增加职工
	void showWorker();//显示职工
	void deleteWorker();//删除职工
	void changeWorker();//修改职工
	void findWorker();//查找职工
	void sortWorker();//排序
	void deleteAll();//清空文档
	void save();//保存文件到文本中
	void getNum();//计算文件内人数
	void init_Emp();//初始化m_EmpArray
	int findPosition(int id);//寻找对应id的员工所在数组中的位置
	bool isExist(int id);//判断员工是否存在
	~WorkManager();//析构
	int m_num=0;//记录文件中原有的员工个数
	Worker ** m_EmpArray;//指向员工数组的指针
	bool isEmpty;
};

