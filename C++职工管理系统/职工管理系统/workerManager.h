#pragma once//��ֹͷ�ļ��ظ�����
#include<iostream>
#include<string>
#include"worker.h"
#include<fstream>
#define FILE "empFile.txt"
using namespace std;

class WorkManager {
public:
	//��ͷ�ļ�����������cpp��ʵ��
	WorkManager();//����
	void showMenu();//��ʾ��ҳ��
	int Exit();//�˳�����
	void addWorker();//����ְ��
	void showWorker();//��ʾְ��
	void deleteWorker();//ɾ��ְ��
	void changeWorker();//�޸�ְ��
	void findWorker();//����ְ��
	void sortWorker();//����
	void deleteAll();//����ĵ�
	void save();//�����ļ����ı���
	void getNum();//�����ļ�������
	void init_Emp();//��ʼ��m_EmpArray
	int findPosition(int id);//Ѱ�Ҷ�Ӧid��Ա�����������е�λ��
	bool isExist(int id);//�ж�Ա���Ƿ����
	~WorkManager();//����
	int m_num=0;//��¼�ļ���ԭ�е�Ա������
	Worker ** m_EmpArray;//ָ��Ա�������ָ��
	bool isEmpty;
};

