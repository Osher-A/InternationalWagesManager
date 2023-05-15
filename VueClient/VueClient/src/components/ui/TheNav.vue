<template>
    <header>
      <nav>
        <h4>International Wages Manager</h4>
        <ul>
          <li>
            <RouterLink to="/employees">Employees</RouterLink>
          </li>
          <li>
            <a href="">Work Conditions</a>
          </li>
          <li>
            <a href="">Salary Components</a>
          </li>
          <li>
            <a href="">Payments</a>
          </li>
          <li>
            <a href="">Statements</a>
          </li>
        </ul>
      </nav>
    </header>
  </template>
  
  <style scoped>
  header {
    width: 100%;
    height: 4.5rem;
    background-color: #30311d;
    display: flex;
    justify-content: center;
    align-items: center;
  }
  
  header a {
    text-decoration: none;
    color: #f391e3;
    display: inline-block;
    padding: 0.75rem 1.5rem;
    border: 1px solid transparent;
  }
  
  a:active,
  a:hover,
  a.router-link-active {
    border: 1px solid #f391e3;
  }
  
  h4{
    color: white;
    margin-left: 0;
  }
  
  header nav {
    width: 90%;
    margin: auto;
    display: flex;
    justify-content: space-between;
    align-items: center;
  }
  
  header ul {
    list-style: none;
    margin: 0;
    padding: 0;
    display: flex;
    justify-content: center;
    align-items: center;
  }
  
  li {
    margin: 0;
  }
  </style>
