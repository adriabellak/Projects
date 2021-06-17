# Agustín Pumarejo Ontañón, Sebastián Juncos Leunig, Adriana Abella Kuri

# Juego STEAM
Start with a descriptive and succinct name for your project. Change the above with the name of your project

# Table of content

- [Introduction](#introduction)
    - [Purpose](#purpose)
    - [Scope](#scope)
    - [Definitions and Acronyms](#definitions-and-acronyms)
- [Overall Description](#overall-description)
    - [User classes](#user-classes)
    - [Assumptions and Dependencies](#assumptions-and-dependencies)
- [System Features and Requirements](#system-features-and-requirements)
    - [Functional Requirements](#functional-requirements)
    - [External Interface Requirements](#external-interface-requirements)
    - [Non-functional Requirements](#non-functional-requirements)
- [Screens](#screens)
    - [Wireframes](#wireframes)

_Table of content generated using VSCode plugin [Markdown TOC](https://marketplace.visualstudio.com/items?itemName=AlanWalk.markdown-toc)_

# Introduction

## Purpose
Promover que alumnos de preparatoria o más jovenes escojan carreras STEAM. Se desarrollara un videojuego que sea atrativo y que provoque interes en un área STEAM.

## Scope
Alumnos mexicanos entre 9 y 18 años.

## Definitions and Acronyms
STEAM: ciencias, tecnologías, ingeniería, artes y matemáticas (por sus siglas en inglés)

# Overall Description
Un nuevo videojuego original de circuitos donde el jugador escoja una combinación de compuertas lógcias para completar el nivel

## User classes
Jugadores: Alumno que utilizará el juego

Analista de STEAM: Miembro de la OSF que verá el progreso de los jugadores en el juego 

## Assumptions and Dependencies
Suposiciones: 
Desarrollar una habilidad relacionada con un área de conocimiento genera un interés en esa área.

El jugador tiene una conexión a internet, un navegador con los requerimientos mínimos para correr el juego.

Are there any assumptions you’re making that could turn out to be false? You should include those here, as well.

Finally, you should note if your project is dependent on any external factors. This might include software components you’re reusing from another project.

# System Features and Requirements
This is where you detail the specific requirements for building your product.

## Functional Requirements
The functional requirements describe the services and functions of a system. Functional requirements must be precise and unambiguous.

El juego debe calcular el puntaje de los usuarios, utilizando el tiempo como parámetro.

El juego debe ser capaz de mandar la información recabada a un analista de STEAM.

El jugador debe poder iniciar un nivel.

El jugador debe poder pausar el juego.

El jugador debe poder salir del juego cuando este lo desee.

Los niveles se irán desbloqueando conforme el jugador progrese en el juego.

El jugador podrá escoger cualquier nivel que ya haya superado.

Se requerirá crear un usuario para comenzar el juego, este podrá retomar el juego despues de hacer login.

El usuario podrá ajustar el volumen de los efectos y la música.

Include user stories, which are short descriptions of a feature, told from the perspective of one of your end user profiles. They are typically structured in the following fashion:

> As a __[type of user]__, I want __[some goal]__ so that __[some reason]__.

You may want to use the following template table.

|Title|User story|Importance|Notes|
|---|---|---|---|
|Alumno de preparatoria|Instrucciones rápidas antes de interactuar con las mecánicas del juego|Permite al jugador entender las reglas del juego de manera fácil e intuitiva|debemos hacer estas instrucicones lo mas sencillas posibles para que los usuarios entiendan las reglas del juego en menos 5 minutos|
|Analista de STEAM|Un juego con indicadores del puntaje de cada usuario dependiendo del tiempo que le toma en resolver el problema|Para medir el  desempeño de cada jugador|N\A|
|_Short identifier_|_As a [type of user], I want [some goal] so that [some reason]_|_Must have_|_Write here any additional consideration_|

## External Interface Requirements
External interface requirements are types of functional requirements. They outline how your product will interface with other components or systems.

There are several types of interfaces you may have requirements for, including:
- User
- Hardware
- Software
- Communications

## Non-functional Requirements
Non-functional requirements are restrictions on the system or the development process. Non-functional requirements can be more critical than functional ones. If they are not met, the system is useless!
El sistema debe ser responsivo ante el input, el retraso debe ser menor a 400 milisegundos.

El juego debe poder correr en los navegadores mas populares (Google chrome, Mozilla Firefox, Safari y Microsoft Edge).

# Screens
Identifying the individual screens (for an app), or pages (for a website) are where a product’s shape starts to become clear. They are a distillation of the user stories into a set of distinct sections that satisfy the needs and behaviors identified so far. The process of outlining an application’s screens may also highlight any requirements or considerations that have been overlooked up to this point.

###Juego:

Menu de pausa

Seleccion de nivel

Pantalla con el nivel actual

Ajustes

###Página:

Login/Register

Videojuego.

Acerca de STEAM.

Resultados recabados (Solo para analistas de STEAM).

Reglas explicación e información del juego.

This has the dual purpose of both contributing to a more accurate vision of the product early on, and serving as a jumping-off point for the time when designers do get involved.

## Wireframes
Wireframes are simple page layouts that outline the size and placement of elements, and features on a page. They are generally devoid of color, font styles, logos or any design elements.

Wireframing is probably the most time-consuming step of this process and for some simple projects, it may be overkill. For complex projects where serious design thinking needs to happen, wireframes are an indispensable tool.

Here are some popular tools for wireframing:
- https://marvelapp.com/  
- https://balsamiq.com/ 
- https://jetstrap.com/ 
- https://www.fluidui.com/ 
- https://ninjamock.com/ 
- https://www.justinmind.com/ 
- https://moqups.com/