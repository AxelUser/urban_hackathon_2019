import React from 'react';
import Auth from './components/Auth/auth';
import Person from './components/Person/person';
import Profile from './components/Profile/profile';
import { BrowserRouter as Router, Route } from 'react-router-dom';

const App: React.FC = () => {
  return (
    <Router>
      <Route exact path="/" component={Auth}/>
      <Route path="/person" component={Person}/>
      <Route path="/profile" component={Profile}/>
      <div className="App">
        <header className="App-header"/>
      </div>
    </Router>
  );
}

export default App;
