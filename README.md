DishCraft

DishCraft is a full-stack web application built with .NET and React, designed to help users manage ingredients and recipes, along with tracking nutritional and pricing information. Users can create recipes from a list of ingredients, calculate price and nutrition per serving, and plan weekly meals to monitor spending and dietary intake.
Features

    Ingredient Management: Add, update, and delete ingredients with associated nutritional and pricing details.​
    GitHub+1GitHub+1

    Recipe Creation: Build recipes by selecting from existing ingredients, specifying quantities, and automatically calculating total cost and nutrition.​

    Per Serving Analysis: View detailed information on price and nutritional content per serving for each recipe.​

    Weekly Meal Planning: Organize recipes into weekly plans to track overall spending and nutritional intake.​

    CRUD Operations: Full Create, Read, Update, and Delete functionality for ingredients, recipes, and weekly plans.​

Technology Stack

    Frontend: React, React Router, Axios​
    GitHub

    Backend: .NET Core Web API​

    Database: Entity Framework Core with SQL Server​

    Authentication: JWT-based authentication (optional)​
    GitHub+2Make a README+2GitHub+2

Getting Started
Prerequisites

    .NET 6 SDK​

    Node.js (v14 or higher)​

    SQL Server​

Installation

    Clone the repository:

    git clone https://github.com/yourusername/dishcraft.git
    cd dishcraft

    Set up the backend:

        Navigate to the backend directory:​

cd backend

Configure the database connection string in appsettings.json.​

Apply migrations and update the database:​

dotnet ef database update

Run the backend server:​
GitHub+2GitHub+2Make a README+2

    dotnet run

Set up the frontend:

    Navigate to the frontend directory:​

cd ../frontend

Install dependencies:​
GitHub+1GitHub+1

npm install

Start the development server:​
GitHub+1GitHub+1

        npm start

        Open your browser and navigate to http://localhost:3000.​

Usage

    Add Ingredients: Navigate to the Ingredients section to add new ingredients, specifying their nutritional values and cost.​

    Create Recipes: In the Recipes section, create new recipes by selecting from existing ingredients and defining quantities.​

    Analyze Recipes: View detailed information on each recipe, including total and per-serving nutrition and cost.​

    Plan Weekly Meals: Use the Weeks section to organize recipes into weekly plans, helping you monitor your dietary intake and spending.​

Contributing

Contributions are welcome! Please follow these steps:

    Fork the repository.​

    Create a new branch: git checkout -b feature/your-feature-name.​

    Commit your changes: git commit -m 'Add your feature'.​

    Push to the branch: git push origin feature/your-feature-name.​

    Open a pull request.​

License

This project is licensed under the MIT License. See the LICENSE file for details.​
Make a README+2GitHub+2
