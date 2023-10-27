const app = document.querySelector("#app")

let title = document.createElement("h3")
title.classList.add("title")
title.innerText = `My Applications`
title.style = `display: flex;`

app.append(title)

const cards = [
  {
  name: `Soul Keeper`,
  img: `https://cdn.discordapp.com/app-icons/341297992797650954/3b8cd7e316be03b2c9963fa16411e305.png?size=512`
  }, {
  name: `SOUP`,
  img: "https://cdn.discordapp.com/app-icons/374639139225600003/a22518085a34fdb43a1859fc25062292.png?size=512"
  }, {
  name: `Shelly`,
  img: "https://cdn.discordapp.com/app-icons/800995120173678643/9bd488186ed855acc1bd19001998bdc0.png?size=512"
  }, {
  name: "Gwynael",
  img: "https://cdn.discordapp.com/app-icons/828318706898960384/753914b96c3bed467ff9f381a05cf091.png?size=512"
  }, {
  name: "AutoStar",
  img: "https://cdn.discordapp.com/app-icons/857760406641573889/d385ff5e7e7c9cbb0cde8d3feb33abd3.png?size=512"
  }, {
  name: "Needed",
  img: "https://cdn.discordapp.com/app-icons/860376188945825802/c2d0a076627e7099493029f46a554317.png?size=512"
  }, {
  name: "Figma",
  img: "https://cdn.discordapp.com/app-icons/873587028019785759/f8e806c7de3b3821feed3bc3724118b6.png?size=512"
  }
]

const genCards = () => {
  
  let cardsContainer = document.createElement("div")
  cardsContainer.classList.add("cards-container")
  
  app.appendChild(cardsContainer)
  
  cards.forEach(card => {
    let cardEl = document.createElement("div")
    cardEl.classList.add("card")
  
    let thumbnailEl = document.createElement("div")
    thumbnailEl.classList.add("thumbnail")
    thumbnailEl.style.backgroundImage = `url(${card.img})`
    
    let label = document.createElement("div")
    label.innerText = card.name
    label.classList.add("label")
    
    cardEl.appendChild(thumbnailEl)
    cardEl.appendChild(label)
    
    cardsContainer.appendChild(cardEl) //test
  })
}

genCards()