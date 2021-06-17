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
function addRandomGreeting() {
  const greetings =
      ["🌮 My hometown is Puebla, México.", "♎ I was born on September 29th, 1999.", "🎹 I play the piano and I'm learning how to play the guitar.", "🤓 I like learning languages. I'm currently an English tutor.", "🐈 I love cats and I would like to adopt one someday.", '🎮 My favorite videogame is The Last of Us.', '🎬 My favorite movies are The Others, Fightclub, and Inglorious Basterds.', "🐶 I named my dog Kala after Tarzan's mom.", "😀 I have an older brother and a younger sister.", "📸 I enjoy taking pictures.", "🐿 Some of my friends say I remind them of a squirrel.", "🎨 I like to draw but I don't do it very often.", "🎧 I like a lot of different music genres, but some of my favorite artists are Coldplay, Radiohead, Tame Impala, Avenged Sevenfold and The 1975."];

  // Pick a random greeting.
  const greeting = greetings[Math.floor(Math.random() * greetings.length)];

  // Add it to the page.
  const greetingContainer = document.getElementById('fact-container');
  greetingContainer.innerText = greeting;
}

function randomizeImage() {
  // The images directory contains 13 images, so generate a random index between
  // 1 and 13.
  const imageIndex = Math.floor(Math.random() * 13) + 1;
  const imgUrl = 'images/kala/kala-' + imageIndex + '.jpg';

  const imgElement = document.createElement('img');
  imgElement.src = imgUrl;

  const imageContainer = document.getElementById('kala-random');
  // Remove the previous image.
  imageContainer.innerHTML = '';
  imageContainer.appendChild(imgElement);
}

// Fetch function
async function randomSongRecommendation() {
    const serverResponse = await fetch("/songs");

    const responseJson = await serverResponse.json();

    const container = document.getElementById('servlet-text-container');

    const jsonLength = Object.keys(responseJson).length;

    const randomSong = responseJson[Math.floor(Math.random() * jsonLength)];

    container.innerText = randomSong;
}

