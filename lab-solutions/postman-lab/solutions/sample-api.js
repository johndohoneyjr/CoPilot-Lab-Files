// Simple API to for a todo list application
const express = require('express');
const bodyParser = require('body-parser');
const uuidv4 = require('uuid/v4');

const app = express();
app.use(bodyParser.json());

// Store the todos in memory
const todos = [];

const findTodo = (id) => {
  for (let i = 0; i < todos.length; i++) {
    if (todos[i].id === id) {
      return todos[i];
    }
  }
  return null;
};

// List all the todos
app.get('/todos', (req, res) => {
  res.json(todos);
});

// Create a new todo
app.post('/todos', (req, res) => {
  const todo = req.body;
  todo.id = uuidv4();
  todos.push(todo);
  res.json(todo);
});

// Update an existing todo
app.put('/todos/:id', (req, res) => {
  const todo = findTodo(req.params.id);
  if (todo) {
    Object.assign(todo, req.body);
    res.json(todo);
  } else {
    res.status(404).send();
  }
});

// Delete a todo
app.delete('/todos/:id', (req, res) => {
  const todo = findTodo(req.params.id);
  if (todo) {
    todos.splice(todos.indexOf(todo), 1);
    res.json(todo);
  } else {
    res.status(404).send();
  }
});

app.listen(3000, () => {
  console.log('Listening on port 3000');
});