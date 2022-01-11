# Projects
## Circuit Sandbox Videogame
This is an educational videogame that teaches players how circuits and logic gates work, and allows them to create their own circuits as well. Inside this folder you will find another README.md file that explains how to run the project locally.

## Software-Product-Sprint-Portfolio
This is a personal project created during my participation in Google's SPS Program in spring of 2021. It's my own personal portfolio that includes information about me, the things I like and my projects. It allows users to send me a message and to interact with me by both submitting and receiving song recommendations. <br>
URL: https://sps-portfolio-331119.uc.r.appspot.com/

## Traffic-Simulation
Traffic simulation. It has two directories, the Mesa one which creates server and it can be used to visualize the simulation through a webpage and Traffic Model which is a Unity project that calls the server and displays the simulation using 3D graphics. **It also contains demo.mp4 which shows how the program works.**

### How to run
The simplest way to run it is directly with the mesa visualization module which only requires anaconda or miniconda and can be done in the following way.

1. Pull the repository
2. Create an environment using the environment.yml file and activate it.

`conda env create -f environment.yml`

`conda activate TrafficModel`

3. Run the server.py script

`python .\server.py`
    
If you want to run the simulation in Unity, you need to install it and open a project from the Traffic Model folder using version 2020.3.22f1. Then, repeat the steps above but instead run the unity.py script in the end and once the server is running, open Unity, go to the BuildCity scene and run it.

## Foodies Mutual Aid
This is a team project created during my participation in Google's SPS Program in spring of 2021. It's a website dedicated to help people find food, allowing them to filter the information by city and send their own submissions. <br>
URL: https://foodies-mutual-aid.uc.r.appspot.com

## CSV Database Query
This is a program that searches through a CSV database of over 36,000 entries using BST and graphs, in order to detect bot attacks, malicious connections or unusual activities in a network. 

## CSS Syntax Highlighter
This is a program that takes several CSS files at a time and creates HTML documents with highlighted CSS syntax using parallel programming
