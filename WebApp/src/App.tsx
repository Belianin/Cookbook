import React, {useState} from 'react';
import './App.css';
import {RecipeItem, Recipe, TagDescription} from "./components/Recipe";

const App: React.FC = () => {
  const tags = {
      "country": {
        "korea": {
          title: "Корея"
        }
      },
      "taste": {
        "spicy": {
          title: "Острое 🔥"
        }
      }
    }


  const recipe: Recipe = {
    title: "Кимчи",
    tags: [
      {
        category: "country",
        id: "korea"
      },
      {
        category: "taste",
        id: "spicy"
      }
    ],
    description: "Очень вкусное острое корейское блюдо",
    ingredients: [
      {
        name: "Капуста",
        count: 1,
        units: "Качан"
      },
      {
        name: "Красный перец",
        count: 100,
        units: "Грамм"
      },
      {
        name: "Соль",
        count: 1,
        units: "Ложка"
      }
    ],
    blocks: [
      {
        title: "Сварить капусту",
        content: "Открыть крышку, налить воду с капустой и включить огонь"
      },
      {
        title: "Смешать с приправами",
        content: "Высыпать все приправы все приправы все приправы все приправы все приправы все приправы"
      }
    ]
  }

  return <div>
    <h1>Project Angel</h1>
    <div className={"centered"}>
      <RecipeItem recipe={recipe} tags={tags} />
    </div>
  </div>
}

export default App;
