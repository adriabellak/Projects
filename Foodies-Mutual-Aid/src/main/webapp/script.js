// Copyright 2020 Google LLC
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     https://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

/**
 * Adds a random greeting to the page.
 */
function addRandomGreeting() 
{
  const greetings =
      ["I'm originally from the suburbs of Chicago!", "I'm the youngest sibling!", 
      "I skipped kindergarten!", "The longest road trip I've taken was 4 days to get to Mexico!"];

  // Pick a random greeting.
  const greeting = greetings[Math.floor(Math.random() * greetings.length)];

  // Add it to the page.
  const greetingContainer = document.getElementById('greeting-container');
  greetingContainer.innerText = greeting;
}

async function sayHello() 
{
    const responseFromServer = await fetch("/hello");
    const textFromResponse = await responseFromServer.text();

    const helloContainer = document.getElementById('hello-container');
    helloContainer.innerText = textFromResponse;
}

async function pullWeather() {
  const responseFromServer = await fetch('/weather');
  const phenomena = await responseFromServer.json();

  const weather = phenomena[Math.floor(Math.random() * phenomena.length)];
  
  const weatherListElement = document.getElementById('weather-container');
  weatherListElement.innerHTML = '';

  weatherListElement.appendChild(
      createListElement('Weather event: ' + weather.weather));
  weatherListElement.appendChild(
      createListElement('Season it occurs: ' + weather.season));

}

/** Creates an <li> element containing text. */
function createListElement(text) 
{
  const liElement = document.createElement('li');
  liElement.innerText = text;
  return liElement;
}

function translate_spanish() {
        const text = document.getElementById('text').value;
        const languageCode = "es";

        const resultContainer = document.getElementById('spanish_text');
        resultContainer.innerText = '';

        const params = new URLSearchParams();
        params.append('text', text);
        params.append('languageCode', languageCode);

        fetch('/translate', 
        {method: 'POST',
          body: params})
          .then(response => response.text())
          .then((translatedMessage) => 
          {resultContainer.innerText = translatedMessage;});
}

function loadFromCities(selectedCity) {
    window.location.href = "food.html";
    document.getElementById(city-input).value = selectedCity;
}

async function loadFoodEntries(selectedCity) {
    const servlet = "/load-" + selectedCity;

    const serverResponse = await fetch(servlet);

    const responseJson = await serverResponse.json();

    document.getElementById('city-container').textContent = "You're looking at the options in " + selectedCity;

    const container = document.getElementById('food-entries');

    container.textContent = '';

    for (i = 0; i < Object.keys(responseJson).length; i++) {
        container.appendChild(createFoodElement(responseJson[i]));
    }
}

function createFoodElement(food) {
    const foodElement = document.createElement('li');
    //foodElement.className = 'class';
    foodElement.innerText = food;
    return foodElement;
}

function showAlert(message) {
    alert(message);
}