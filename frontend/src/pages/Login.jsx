import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { toast, ToastContainer } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
import { setToken } from '../utility/storage' // Import the utility function

export default function Login() {
  const navigate = useNavigate();
  const [username, setUsername] = useState('');
  const [password, setPassword] = useState('');
  const [rememberMe, setRememberMe] = useState(false);

  const handleSubmit = async (event) => {
    event.preventDefault();
    const requestData = {
      Username: username,
      Password: password,
    };
    try {
      const response = await fetch('http://localhost:5188/login', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
          'accept': '*/*',
        },
        body: JSON.stringify(requestData),
      });

      const data = await response.json();

      if (response.ok) {
        setToken(data.token, rememberMe); // Use the utility function to set the token
        toast.success('Login successful', { autoClose: 3000, containerId: 'login' });
        setTimeout(() => {
          navigate('/');
        }, 1000);
      } else {
        toast.error(data.message || 'Login failed', { autoClose: 3000, containerId: 'login' });
      }
    } catch (error) {
      toast.error('An error occurred. Please try again.', { autoClose: 3000, containerId: 'login' });
    }
  };

  return (
    <div className='flex justify-center bg-gradient-to-br from-sky-700 to-sky-800 items-center h-full w-full'>
      <div className='bg-gray-800 rounded-3xl flex flex-col justify-center shadow-lg shadow-gray-700/50'>
        <form className='relative min-w-[600px] w-full mx-auto rounded-3xl bg-gray-900 p-10 px-10 shadow-lg shadow-gray-700/50' onSubmit={handleSubmit}>
          <h2 className='absolute top-4 left-4 text-2xl text-blue-400 font-bold'>GL-ERP</h2>
          <h2 className='text-4xl dark:text-white font-bold text-center mt-8'>SIGN IN</h2>
          <div className='flex flex-col text-gray-400 py-2'>
            <label>Username</label>
            <input
              className='rounded-lg bg-gray-700 mt-2 p-2 focus:border-blue-500 focus:bg-gray-800 focus:outline-none'
              type="text"
              value={username}
              onChange={(e) => setUsername(e.target.value)}
              required
            />
          </div>
          <div className='flex flex-col text-gray-400 py-2'>
            <label>Password</label>
            <input
              className='p-2 rounded-lg bg-gray-700 mt-2 focus:border-blue-500 focus:bg-gray-800 focus:outline-none'
              type="password"
              value={password}
              onChange={(e) => setPassword(e.target.value)}
              required
            />
          </div>
          <div className='flex justify-between text-gray-400 py-2'>
            <p className='flex items-center'>
              <input
                className='mr-2'
                type="checkbox"
                checked={rememberMe}
                onChange={(e) => setRememberMe(e.target.checked)}
              /> Remember Me
            </p>
          </div>
          <button className='w-full my-5 py-2 bg-teal-500 shadow-lg shadow-teal-500/50 hover:shadow-teal-500/40 text-white font-semibold rounded-lg'>SIGN IN</button>
        </form>
      </div>
      <ToastContainer containerId="login" />
    </div>
  );
}