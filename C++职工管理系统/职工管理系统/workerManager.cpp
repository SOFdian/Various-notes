#include<iostream>
#include<string>
#include"workerManager.h"
#include"worker.h"
#include"employee.h"
#include"Manager.h"
#include"Boss.h"
using namespace std;

WorkManager::WorkManager() {
	//1.�޳�ʼ�ļ�
	ifstream ifs;
	ifs.open(FILE, ios::in);
	if (!ifs.is_open()) {
		this->m_num = 0;//0��
		this->m_EmpArray = NULL;
		this->isEmpty = true;
		cout << "���棺�޳�ʼ�ļ���������" << endl;
		ifs.close();
		return;//����
	}
	//2.��ʼ�ļ�Ϊ��
	char ch;
	ifs >> ch;//����һ���ַ�������һ���ַ��ǲ��ǽ�β�ַ�
	if (ifs.eof()) {
		this->m_num = 0;//0��
		this->m_EmpArray = NULL;
		this->isEmpty = true;
		cout << "���棺��ʼ�ļ�Ϊ�գ�������" << endl;
		ifs.close();
		return;//����
	}
	//3.��ʼ�ļ���Ϊ��
	//����д�ڵ����ĺ������ܱȽϺã�������ܻ��������Ĳ����ȡ���ݣ�
	init_Emp();
}
//���캯����ʼ��
WorkManager::~WorkManager() {
	if (this->m_EmpArray != NULL) {
		delete[] this->m_EmpArray;
		this->m_EmpArray = NULL;
	}
}
//����
void WorkManager::showMenu() {
	cout << "************************************" << endl;
	cout << "****** ��ӭʹ��ְ������ϵͳ��*******" << endl;
	cout << "***********0.�˳�����ϵͳ***********" << endl;
	cout << "***********1.����ְ����Ϣ***********" << endl;
	cout << "***********2.��ʾְ����Ϣ***********" << endl;
	cout << "***********3.ɾ����ְְ��***********" << endl;
	cout << "***********4.�޸�ְ����Ϣ***********" << endl;
	cout << "***********5.����ְ����Ϣ***********" << endl;
	cout << "***********6.���ձ������***********" << endl;
	cout << "***********7.��������ĵ�***********" << endl;
	cout << "************************************" << endl;
	cout << endl;
}
//�˵���
int WorkManager::Exit() {
	cout << "��ӭ�´�ʹ��" << endl;
	return 0;
}
//�˳�
void WorkManager::addWorker() {
	int num=0,job;
	string name;
	Worker* worker = NULL;
	while (1) {
		cout << "Ա����Ŵ�����-1�������˳�Ա������ϵͳ" << endl;
		cout << "������Ա���ı�ţ���������ֻᵼ�³��������" << endl;
		cin >> num;//������������˷����λᵼ���������ݶ�����num��num��ȻΪ0����������ķ��������ݲ�����ʧ��֮���ֱ��������һ�У���Ϊһֱ��һ������û�ж���������numʼ��Ϊ0���޷����ģ�����ѭ��
		if (num == -1) {
			system("cls");
			this->showMenu();//�ٴ���ʾ�˵�
			break;
		}else if (num < 0) {
			cout << "��������ڵ���0�ı�ţ�" << endl;
		}else if (isExist(num)) {
			cout << "�ñ�ŵ�Ա���Ѵ��ڣ�" << endl;
		}		//������������������
		//���²�����Ч���룬��Ч��������½��������Ž׶�
		else {
			cout << "������Ա��������" << endl;
			cin >> name;
			cout << "������Ա����ְ��" << endl;
			cout << "1.ְ��" << endl;
			cout << "2.����" << endl;
			cout << "3.�ϰ�" << endl;
			cin >> job;
			switch (job)
			{
			case 1:
				worker = new employee(num, name, job);
				break;
			case 2:
				worker = new Manager(num, name, job);
				break;
			case 3:
				worker = new Boss(num, name, job);
				break;
			default:
				cout << "���벻�Ϸ���" << endl;
				break;
			}
			if (job == 1 || job == 2 || job == 3) {
				//�ж�Ա������
				int newsize = this->m_num + 1;//����һ��Ա�����Ա������
				Worker** newspace = new Worker * [newsize];//����������͵Ĺ�ϵ���Ǻܶ�
				/*
				* new��һ��������newsize������ΪWorker*�����ݵĿ����飨����a����������һ��ָ��������ָ��
				* ͬʱnewspace��һ��ָ��ָ���ָ��
				* ����ָ��������������ϵ
				* newspace[i]��a[i],��һ��ָ���ض�Worker��ָ��
				*/
				//������Ҫ������ΪWorker*�����ݴ���newspace
				newspace[newsize - 1] = worker;//��������Ϊ��������newsize������newsize-1�������˷���1Сʱ����ͷ�ٿ���
				//worker��һ��ָ��Worker��ĳ�������ָ�룬���;���Worker*�����䴫��newspace
				if (this->m_EmpArray != NULL) {
					for (int i = 0; i < this->m_num; i++) {
						newspace[i] = this->m_EmpArray[i];
						//��ԭ���������δ���
						//p1[n1]�ȼ���*(p1 + n1)
					}
				}
				delete[] this->m_EmpArray;//�ͷ�����ʱ���ϡ���
				//�ͷ�ԭ���Ŀռ�
				this->m_EmpArray = newspace;
				//ָ�����ڵĿռ�
				this->m_num++;
				cout << "��ӳɹ���" << endl;
				this->isEmpty = false;
				this->save();//����
				this->showWorker();
		}
		}
		
	}
	
}
//���Ա��(��һ������ѭ����bugδ�ų�Ҳδ�ҵ���������)
void WorkManager::showWorker() {
	for (int i = 0; i < m_num; i++)
	{
		//���ö�̬���ýӿ�
		this->m_EmpArray[i]->showInfo();
	}
	
}
//չʾԱ��
void WorkManager::changeWorker(){
	int id;
	cout << "��������Ҫ�޸���Ϣ��Ա�����" << endl;
	cin >> id;
	if (isExist(id)) {
		int i = this->findPosition(id);
		int num, job;
		string name;
		cout << "�������µ�Ա�����" << endl;
		cin >> num;
		cout << "������Ա��������" << endl;
		cin >> name;
		cout << "������Ա����ְ��" << endl;
		cout << "1.ְ��" << endl;
		cout << "2.����" << endl;
		cout << "3.�ϰ�" << endl;
		cin >> job;
		switch (job)
		{
		case 1:
			delete m_EmpArray[i];//ԭ����ɾ��
			m_EmpArray[i] = new employee(num, name, job);//ָ���½���
			this->save();//����
			break;
		case 2:
			delete m_EmpArray[i];
			m_EmpArray[i] = new Manager(num, name, job);
			this->save();
			break;
		case 3:
			delete m_EmpArray[i];
			m_EmpArray[i] = new Boss(num, name, job);
			this->save();
			break;
		default:
			cout << "���벻�Ϸ���" << endl;
			break;
		}
	}
	else {
		cout << "���޴��ˣ�" << endl;
	}
}
//�ı�Ա��
void WorkManager::deleteAll(){
	delete[] m_EmpArray;
	m_EmpArray = NULL;
	m_num = 0;
	isEmpty = true;
	this->save();
}
//�������
void WorkManager::deleteWorker() {
	int id;
	cout << "������Ҫɾ��Ա���ı��" << endl;
	cin >> id;
	if (isExist(id)) {
		if (m_num - 1 == 0) {
			delete[] this->m_EmpArray;//�����Ա����Ψһһ��Ա����ֱ��ɾ�����鲢���ÿ�
			this->m_EmpArray = NULL;
			this->isEmpty = true;
			this->m_num = 0;
		}else {
			Worker** newspace = new Worker * [m_num - 1];
			this->m_num--;
			for (int i = 0, j = 0; i < m_num; j++) {
				if (id != m_EmpArray[j]->number) {
					newspace[i] = m_EmpArray[j];//����ԭ���飨�ų�ɾ��Ա�����浽��������
					i++;
				}
			}
			delete[] this->m_EmpArray;
			this->m_EmpArray = newspace;
		}
		cout << "�����ɹ����ִ�Ա��" << m_num << "�ˣ�" << endl;
		this->save();
	}
	else {
		cout << "���޴��ˣ�" << endl;
	}


}
//ɾ��Ա��
void WorkManager::findWorker(){
	int id;
	cout << "������Ҫ���ҵ�Ա����ţ�" << endl;
	cin >> id;
	if (!isExist(id)) {
		cout << "���޴��ˣ�" << endl;
	}
	else {
		m_EmpArray[findPosition(id)]->showInfo();
	}
}
//����Ա��
void WorkManager::sortWorker(){
	cout << "��ѡ�����з�ʽ" << endl;
	cout << "1.���������" << endl;
	cout << "2.����Ž���" << endl;
	int num;
	int MinOrMax;
	cin >> num;
	for (int i = 0; i < m_num-1; i++) {
		MinOrMax = i;
		for (int j = i + 1; j < m_num; j++) {
			if (num == 1) {//����
				if (m_EmpArray[j]->number < m_EmpArray[i]->number) {//�������е�i��number������number���αȽ�
					MinOrMax = j;//���ֺ���ĵ�j��number�ȵ�i��numberС�ͽ���
				}
			} 
			if (num == 2) {
				if (m_EmpArray[j]->number > m_EmpArray[i]->number) {//�������е�i��number������number���αȽ�
					MinOrMax = j;
				}
			}
			if (MinOrMax != i) {
				Worker* exchange;
				exchange = m_EmpArray[j];
				m_EmpArray[j] = m_EmpArray[i];
				m_EmpArray[i] = exchange;
				MinOrMax = i;//������֮���i��number������С���ˣ�
			}
		}
		cout << "�����ɹ���" << endl;
		this->save();
		this->showWorker();
	}
}
//����
void WorkManager::save() {
	ofstream ofs;
	ofs.open(FILE, ios::out);
	if (isEmpty == false) {//�ǿ�ʱ
		for (int i = 0; i < this->m_num; i++) {
			ofs << this->m_EmpArray[i]->number << " "
				<< this->m_EmpArray[i]->name << " "
				<< this->m_EmpArray[i]->did << endl;
		}
	}
	else {
		ofs << "";
	}
	ofs.close();
	cout << "�ѱ��棡" << endl;
}
//�ļ�д��
void WorkManager::getNum() {
	ifstream ifs;
	ifs.open(FILE, ios::in);

	int id, did;
	string name;
	while (ifs >> id && ifs >> name && ifs >> did) {
		this->m_num++;
	}
}
//�����ļ�������
void WorkManager::init_Emp() {
	ifstream ifs;
	ifs.open(FILE, ios::in);
	int id, did;
	string name;
	getNum();
	//����Ա������
	m_EmpArray = new Worker * [m_num];//������Ӧ����������
	int i = 0;//whileѭ����
	while (ifs >> id && ifs >> name && ifs >> did) {
		if (did == 1) {
			m_EmpArray[i] = new employee(id, name, did);
		}
		else if (did == 2) {
			m_EmpArray[i] = new Manager(id, name, did);
		}
		else if (did == 3) {
			m_EmpArray[i] = new Boss(id, name, did);
		}
		i++;
	}
}
//���ļ���ԭ��Ա������m_EmpArray
int WorkManager::findPosition(int id) {
	for (int i = 0; i < m_num; i++) {
		if (id == m_EmpArray[i]->number) {
			return i;
			//ǰ���ж�idһ�����ڣ�һ����returnһ��i
		}
	}
}
//ǰ��Ҫ�ж�id�Ƿ����
bool WorkManager::isExist(int id) {
	for (int i = 0; i < m_num; i++) {
		if (id == this->m_EmpArray[i]->number) {
			return true;//�������Ƿ��и�id
		}
	}
	return false;//�����궼û�о�return false
}
