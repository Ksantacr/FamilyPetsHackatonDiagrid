// Copyright Amazon.com, Inc. or its affiliates. All Rights Reserved.
// SPDX-License-Identifier: Apache-2.0
import { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import { signIn, signUp } from './authService';
import { 
  Button, 
  Flex, 
  Heading, 
  TextField, 
  View, 
  Divider 
} from '@aws-amplify/ui-react';
import '@aws-amplify/ui-react/styles.css';
import './loginPage.css';
import ThemeToggle from './toggle'; // Importa el componente ThemeToggle

const LoginPage = () => {
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [confirmPassword, setConfirmPassword] = useState('');
  const [isSignUp, setIsSignUp] = useState(false);
  const navigate = useNavigate();

  const handleSignIn = async (e) => {
    e.preventDefault();
    try {
      const session = await signIn(email, password);
      console.log('Sign in successful', session);
      if (session && typeof session.AccessToken !== 'undefined') {
        sessionStorage.setItem('accessToken', session.AccessToken);
        if (sessionStorage.getItem('accessToken')) {
          window.location.href = '/home';
        } else {
          console.error('Session token was not set properly.');
        }
      } else {
        console.error('SignIn session or AccessToken is undefined.');
      }
    } catch (error) {
      alert(`Sign in failed: ${error}`);
    }
  };

  const handleSignUp = async (e) => {
    e.preventDefault();
    if (password !== confirmPassword) {
      alert('Passwords do not match');
      return;
    }
    try {
      await signUp(email, password);
      navigate('/confirm', { state: { email } });
    } catch (error) {
      alert(`Sign up failed: ${error}`);
    }
  };

  return (
    <Flex
      direction="column"
      alignItems="center"
      justifyContent="center"
      padding="large"
      className="page-container"
    >
      <ThemeToggle /> {/* Agrega el componente ThemeToggle aquí */}
      <View className="card">
        <Heading level={3} textAlign="center">{isSignUp ? 'Create Account' : 'Sign In'}</Heading>
        <Divider marginTop="medium" marginBottom="medium" />
        <form onSubmit={isSignUp ? handleSignUp : handleSignIn}>
          <TextField
            name="email"
            label="Email"
            type="email"
            value={email}
            onChange={(e) => setEmail(e.target.value)}
            placeholder="Enter your email"
            required
            marginBottom="medium"
          />
          <TextField
            name="password"
            label="Password"
            type="password"
            value={password}
            onChange={(e) => setPassword(e.target.value)}
            placeholder="Enter your password"
            required
            marginBottom="medium"
          />
          {isSignUp && (
            <TextField
              name="confirmPassword"
              label="Confirm Password"
              type="password"
              value={confirmPassword}
              onChange={(e) => setConfirmPassword(e.target.value)}
              placeholder="Confirm your password"
              required
              marginBottom="medium"
            />
          )}
          <Button type="submit" variation="primary" isFullWidth>{isSignUp ? 'Sign Up' : 'Sign In'}</Button>
        </form>
        <Divider marginTop="medium" marginBottom="medium" />
        <Button onClick={() => setIsSignUp(!isSignUp)} variation="link" isFullWidth>
          {isSignUp ? 'Already have an account? Sign In' : 'Need an account? Sign Up'}
        </Button>
      </View>
    </Flex>
  );
};

export default LoginPage;
