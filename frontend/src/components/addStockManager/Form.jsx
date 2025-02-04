import React, { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import styled from 'styled-components';
import { toast } from 'react-toastify';
import { Trash2, UserPlus, Search } from 'lucide-react';
import { motion } from "framer-motion";
import { getToken } from '../../utility/storage'; // Ensure this import is correct

const capitalize = (str) => {
  return str.charAt(0).toUpperCase() + str.slice(1);
};

const Form = () => {
  const [firstName, setFirstName] = useState('');
  const [lastName, setLastName] = useState('');
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [confirmPassword, setConfirmPassword] = useState('');
  const [errors, setErrors] = useState({});
  const [stockManagers, setStockManagers] = useState([]);
  const [searchTerm, setSearchTerm] = useState('');
  const [isLoggedIn, setIsLoggedIn] = useState(true); // Replace with actual login state
  const [token, setToken] = useState(getToken());

  const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
  const passwordRegex = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$/; // Minimum 8 characters, at least one uppercase letter, one lowercase letter, one number, and one special character
  const navigation = useNavigate();

  useEffect(() => {
    if (isLoggedIn) {
      fetchStockManagers();
    }
  }, [isLoggedIn]);

  const fetchStockManagers = async () => {
    if (!token) {
      console.log('User is not authenticated. Skipping fetch.');
      return;
    }
    try {
      console.log(`Bearer ${token}`);
      const response = await fetch('http://localhost:5188/api/Admin/getstockmanagers', {
        method: 'GET',
        headers: {
          'Authorization': `Bearer ${token}`,
          'Content-Type': 'application/json'
        }
      });
      if (!response.ok) {
        console.error('Response status:', response.status);
        console.error('Response status text:', response.statusText);
        throw new Error('Network response was not ok');
      }
      const data = await response.json();
      if (Array.isArray(data)) {
        setStockManagers(data);
        console.log('Stock managers:', data);
      } else {
        throw new Error('Response is not an array');
      }
    } catch (error) {
      console.error('Error fetching stock managers:', error);
    }
  };

  const validateForm = () => {
    const newErrors = {};

    if (!emailRegex.test(email)) {
      newErrors.email = 'Invalid email format';
    }

    if (!passwordRegex.test(password)) {
      newErrors.password = 'Password must be at least 8 characters long and contain at least one uppercase letter, one lowercase letter, one number, and one special character';
    }

    if (confirmPassword !== password) {
      newErrors.confirmPassword = 'Passwords do not match';
    }

    setErrors(newErrors);

    return Object.keys(newErrors).length === 0;
  };

  const handleFirstName = (event) => {
    setFirstName(event.target.value);
  };

  const handleLastName = (event) => {
    setLastName(event.target.value);
  };

  const handleEmail = (event) => {
    setEmail(event.target.value);
  };

  const handlePassword = (event) => {
    setPassword(event.target.value);
  };

  const handleConfirmPassword = (event) => {
    setConfirmPassword(event.target.value);
  };

  const handleSubmit = async (event) => {
    event.preventDefault();
    if (validateForm()) {
      const newManager = { firstName, lastName, password, email };
      if (!token) {
        console.log('User is not authenticated. Skipping fetch.');
        return;
      }
      try {
        const response = await fetch('http://localhost:5188/api/Admin/addnewstockmanager', {
          method: 'POST',
          headers: {
            'Authorization': `Bearer ${token}`,
            'Content-Type': 'application/json'
          },
          body: JSON.stringify(newManager)
        });
        if (!response.ok) {
          console.error('Response status:', response.status);
          console.error('Response status text:', response.statusText);
          throw new Error('Network response was not ok');
        }
        const data = await response.json();
        setStockManagers([...stockManagers, { userName: `${newManager.firstName} ${newManager.lastName}`, email: newManager.email }]);
        toast.success('Admin added successfully');
        console.log('saye tzed Form submitted successfully');
        // Clear form fields
        setFirstName('');
        setLastName('');
        setEmail('');
        setPassword('');
        setConfirmPassword('');
      } catch (error) {
        console.error('Error adding stock manager:', error);
        toast.error('Failed to add admin');
      }
    } else {
      toast.error('Admin not added');
      console.log('Form validation failed');
    }
  };

  const handleDelete = async (email) => {
    if (!token) {
      console.log('User is not authenticated. Skipping delete.');
      return;
    }
    try {
      const response = await fetch(`http://localhost:5188/api/Admin/deletestockmanager?email=${email}`, {
        method: 'DELETE',
        headers: {
          'Authorization': `Bearer ${token}`
        }
      });
      if (!response.ok) {
        console.error('Response status:', response.status);
        console.error('Response status text:', response.statusText);
        throw new Error('Network response was not ok');
      }
      setStockManagers(stockManagers.filter(manager => manager.email !== email));
      toast.success('Admin deleted successfully');
    } catch (error) {
      console.error('Error deleting stock manager:', error);
      toast.error('Failed to delete admin');
    }
  };

  const handleSearch = (e) => {
    const term = e.target.value.toLowerCase();
    setSearchTerm(term);
  };

  const filteredManagers = stockManagers.filter(
    (manager) =>
      manager.userName?.toLowerCase().includes(searchTerm) ||
      manager.email?.toLowerCase().includes(searchTerm)
  );

  return (
<div className="flex flex-col items-center justify-center pt-28 min-h-screen w-full bg-[#e2e2e2]">
<StyledWrapper >
        <form className="form" onSubmit={handleSubmit}>
          <p className="text-center title">Add Stock Manager</p>
          <div className="flex">
            <label>
              <input className="input" type="text" value={firstName} onChange={handleFirstName} placeholder="" required />
              <span>Firstname</span>
            </label>
            <label>
              <input className="input" type="text" value={lastName} onChange={handleLastName} placeholder="" required />
              <span>Lastname</span>
            </label>
          </div>
          <label>
            <input className="input" type="email" value={email} onChange={handleEmail} placeholder="" required />
            <span>Email</span>
          </label>
          <label>
            <input className="input" type="password" value={password} onChange={handlePassword} placeholder="" required />
            <span>Password</span>
          </label>
          <label>
            <input className="input" type="password" value={confirmPassword} onChange={handleConfirmPassword} placeholder="" required />
            <span>Confirm password</span>
          </label>
          <button className="submit">Submit</button>
        </form>
      </StyledWrapper>

      <motion.div
        className="mt-4 bg-gradient-to-br from-sky-800 to-sky-900 bg-opacity-50 backdrop-blur-md shadow-lg rounded-xl p-6 border mb-8"
        initial={{ opacity: 0, y: 0 }}
        animate={{ opacity: 1, y: 0 }}
        transition={{ duration: 0.4 }}
      >
        <div className="flex justify-between items-center mb-6">
          <UserPlus size={50} style={{ color: "#8B5CF6", minWidth: "50px" }} />
          <h2 className="text-4xl mx-16 font-semibold text-gray-100">Stock Managers</h2>
          <div className="relative">
            <input
              type="text"
              placeholder="Search stock managers..."
              className="bg-gray-700 text-white placeholder-gray-400 rounded-lg pl-10 pr-4 py-2 focus:outline-none focus:ring-2 focus:ring-blue-500"
              onChange={handleSearch}
              value={searchTerm}
            />
            <Search className="absolute left-3 top-2.5 text-gray-400" size={18} />
          </div>
        </div>
        <div className="overflow-x-auto">
          <table className="min-w-full border-spacing-y-4">
            <thead>
              <tr className="text-gray-100">
                <th className="px-6 py-3 text-left text-sm font-medium uppercase">Username</th>
                <th className="px-6 py-3 text-left text-sm font-medium uppercase">Email</th>
                <th className="px-6 py-3 text-left text-sm font-medium uppercase">Actions</th>
              </tr>
            </thead>
            <tbody>
              {filteredManagers.map((manager) => (
                <motion.tr
                  key={manager.email}
                  initial={{ opacity: 0 }}
                  animate={{ opacity: 1 }}
                  transition={{ duration: 0.3 }}
                  className="text-gray-100 hover:bg-gray-800 border-b-[1px] border-gray-700"
                >
                  <td className="px-6 py-4 text-sm">{manager.userName}</td>
                  <td className="px-6 py-4 text-sm">{manager.email}</td>
                  <td className="px-6 py-4 text-sm">
                    <button className="delete-button" onClick={() => handleDelete(manager.email)}>
                      <Trash2 size={30} className="ml-5" color="red" />
                    </button>
                  </td>
                </motion.tr>
              ))}
            </tbody>
          </table>
        </div>
      </motion.div>
    </div>
  );
};

const StyledWrapper = styled.div`
  .form {
    display: flex;
    flex-direction: column;
    gap: 20px;
    width: 550px;
    max-width: 550px;
    height: 500px;
    max-height: 800px;
    padding: 20px;
    border-radius: 20px;
    position: relative;
    background-color: #1a1a1a;
    color: #fff;
    border: 1px solid #333;
  }

  .title {
    text-align: center;
    font-size: 28px;
    font-weight: 600;
    letter-spacing: -1px;
    position: relative;
    display: flex;
    align-items: center;
    padding-left: 30px;
    color: #00bfff;
  }

  .title::before {
    width: 18px;
    height: 18px;
    text-align: center;
  }

  .title::after {
    width: 18px;
    height: 18px;
    text-align: center;
    animation: pulse 1s linear infinite;
  }

  .title::before,
  .title::after {
    position: absolute;
    content: "";
    height: 16px;
    width: 16px;
    border-radius: 50%;
    left: 0px;
    background-color: #00bfff;
  }

  .message,
  .signin {
    font-size: 14.5px;
    color: rgba(255, 255, 255, 0.7);
  }

  .signin {
    text-align: center;
  }

  .signin a:hover {
    text-decoration: underline royalblue;
  }

  .signin a {
    color: #00bfff;
  }

  .flex {
    display: flex;
    justify-content: space-around;
    width: 100%;
  }

  .form label {
    position: relative;
    padding: 0px;
  }

  .form label .input {
    padding: 10px;
    background-color: #333;
    color: #fff;
    width: 100%;
    padding: 20px 05px 05px 10px;
    outline: 0;
    border: 1px solid rgba(105, 105, 105, 0.397);
    border-radius: 10px;
  }

  .form label .input + span {
    color: rgba(255, 255, 255, 0.5);
    position: absolute;
    left: 10px;
    top: 0px;
    font-size: 0.9em;
    cursor: text;
    transition: 0.3s ease;
  }

  .form label .input:placeholder-shown + span {
    top: 12.5px;
    font-size: 0.9em;
  }

  .form label .input:focus + span,
  .form label .input:valid + span {
    color: #00bfff;
    top: 0px;
    font-size: 0.7em;
    font-weight: 600;
  }

  .input {
    font-size: medium;
  }

  .submit {
    border: none;
    outline: none;
    padding: 10px;
    border-radius: 10px;
    color: #fff;
    font-size: 16px;
    transform: 0.3s ease;
    background-color: #00bfff;
    width: 150px;
    margin: auto;
  }

  .submit:hover {
    background-color: #00bfff96;
  }

  .delete-button {
    background-color: red;
    border: none;
    padding: 5px;
    border-radius: 5px;
    cursor: pointer;
  }

  .delete-button:hover {
    background-color: darkred;
  }

  .relative {
    position: relative;
  }

  .relative input {
    padding-left: 2.5rem;
  }

  .relative .Search {
    position: absolute;
    left: 0.75rem;
    top: 0.75rem;
  }

  .overflow-x-auto {
    overflow-x: auto;
  }
`;

export default Form;