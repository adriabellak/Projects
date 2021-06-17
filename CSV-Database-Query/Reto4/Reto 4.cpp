// Agustín Pumarejo Ontañón, A01028997
// Adriana Abella Kuri, A01329591
// Reto 3

// g++ -std=c++11 reto3.cpp -o reto3
// ./reto3

#include <iostream>
#include <vector>
#include <fstream>
#include <sstream>
#include <unordered_map>

using namespace std;

class Record
{
public:
    string date;
    string time;
    string sourceIP;
    string sourcePort;
    string sourceName;
    string destIP;
    string destPort;
    string destName;

    Record(){};

    Record(string _date, string _time, string _sourceIP, string _sourcePort, string _sourceName, string _destIP, string _destPort, string _destName)
    {
        date = _date;
        time = _time;
        sourceIP = _sourceIP;
        sourcePort = _sourcePort;
        sourceName = _sourceName;
        destIP = _destIP;
        destPort = _destPort;
        destName = _destName;
    }

    void printRecord()
    {
        cout << date << ", " << time << ", " << sourceIP << ", " << sourcePort << ", " << sourceName << ", " << destIP << ", " << destPort << ", " << destName << endl;
    }
};

template <class T, class S>
class Node{
    public:
    T key;
    vector<S> values;
    Node<T, S>* left;
    Node<T, S>* right;

    Node(){
        key= NULL;
        values= NULL;
        left= NULL;
        right= NULL;
    }

    Node(T _key, S _value){
        key= _key;
        values.push_back(_value);
        left= NULL;
        right= NULL;
    }

    void printNode(){
        cout << "Página(s): ";
        for(int i= 0; i < values.size(); i++){
            cout << values[i] << ", ";
        }
        cout << endl;
        cout << "Número de entradas: " << key << endl;
    }

    void addValue(vector<S> value){
        values.push_back(value[0]);
    }
};

template <class T, class S>
class BST{
    public:
    Node<T, S>* root;

    BST(){
        root= NULL;
    }

    void insert(T key, S value){
        Node<T, S>* inserted= new Node<T, S>(key, value);
        if(root == NULL){
            root= inserted;
        }
        else{
            auxInsert(inserted, root);
        }
    }

    void auxInsert(Node<T, S>* inserted, Node<T, S>* temp){
        if(temp->key > inserted->key){
            if(temp->left == NULL){
                temp->left= inserted;
            }
            else{
                auxInsert(inserted, temp->left);
            }
        }
        else if(temp->key < inserted->key){
            if(temp->right == NULL){
                temp->right= inserted;
            }
            else{
                auxInsert(inserted, temp->right);
            }
        }
        else{
            temp->addValue(inserted->values);
        }
        return;
    }

    int antiInorder(int n, Node<int,string>* temp){
        if(temp != NULL){
            n= antiInorder(n, temp->right);
            if(n != 0){
                temp->printNode();
                n--;
            }
            n= antiInorder(n, temp->left);
        }
        return n;
    }
};

class RecordManager
{
public:
    vector<Record> entries;
    string order;

    void read(string file)
    {
        ifstream fileIn;
        fileIn.open(file);
        string record, parts;
        vector<string> data;
        while (getline(fileIn, record))
        {
            istringstream sIn(record);
            while (getline(sIn, parts, ','))
            {
                data.push_back(parts);
            }
            if (data[7].find('\r') != data[7].npos)
            {
                data[7] = data[7].substr(0, data[7].size() - 1);
            }
            Record r(dateFixer(data[0]), timeFixer(data[1]), data[2], data[3], data[4], data[5], data[6], data[7]);
            entries.push_back(r);
            data.clear();
        }
    }

    string timeFixer(string badTime)
    {
        string goodTime;
        int pos;
        pos = badTime.find(":");
        if (pos == 1)
        {
            badTime.insert(0, "0");
        }
        goodTime = badTime.substr(0, 3);
        badTime = badTime.substr(3, string::npos);
        pos = badTime.find(":");
        if (pos == 1)
        {
            badTime.insert(0, "0");
        }
        goodTime += badTime.substr(0, 3);
        badTime = badTime.substr(3, string::npos);
        if (badTime.length() == 1)
        {
            badTime.insert(0, "0");
        }
        goodTime += badTime;
        return goodTime;
    }

    string dateFixer(string badTime)
    {
        string goodTime;
        int pos;
        pos = badTime.find("-");
        if (pos == 1)
        {
            badTime.insert(0, "0");
        }
        goodTime = badTime.substr(0, 3);
        badTime = badTime.substr(3, string::npos);
        pos = badTime.find("-");
        if (pos == 1)
        {
            badTime.insert(0, "0");
        }
        goodTime += badTime;
        return goodTime;
    }

    

    unordered_map<string, int> conexionesPorDia(string date){  

        date= dateFixer(date);
        unordered_map<string, int> connections;
        bool found= false;

        for(Record r: entries){
            string destination= r.destName;
            if(r.date == date){
                found= true;
                if(destination != "-" && destination.find("reto.com") == string::npos){
                    if(connections.find(destination) == connections.end()){
                        connections.insert({destination, 1});
                    }
                    else{
                        connections[destination]++;
                    }
                }
            }
            else{
                if(found == true){
                    break;
                }
            }
        }
        return connections;
    }

    void top(int n, string date){

        unordered_map<string, int> connections=conexionesPorDia(date);
        BST<int, string> tree;
        for(pair<string, int> it: connections){
            tree.insert(it.second, it.first);
        }
        Node<int, string>* temp= tree.root;
        tree.antiInorder(n, temp);
    }
};

int main(){
    RecordManager equipo11;
    equipo11.read("nuevo11.csv");

    for(int i= 10; i < 22; i++){
        cout << "Día: " << i << endl;
        equipo11.top(5, to_string(i)+"-08-2020");
        cout << endl;
    }
}