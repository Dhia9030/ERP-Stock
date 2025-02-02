import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import styled from 'styled-components';
import { toast } from 'react-toastify';

const Form = () => {
  const [firstName, setFirstName] = useState('');
  const [lastName, setLastName] = useState('');
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [confirmPassword, setConfirmPassword] = useState('');
  const [errors, setErrors] = useState({});

  const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
  const passwordRegex = /^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$/; // Minimum 8 characters, at least one letter and one number
  const navigation = useNavigate();

  const validateForm = () => {
    const newErrors = {};

    if (!emailRegex.test(email)) {
      newErrors.email = 'Invalid email format';
    }

    if (!passwordRegex.test(password)) {
      newErrors.password = 'Password must be at least 8 characters long and contain at least one letter and one number';
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

  const handleSubmit = (event) => {
    event.preventDefault();
    if (validateForm()) {
      // Perform form submission
      toast.success('Admin added successfully');
      console.log('Form submitted successfully');
      //navigation('/orders');
    } else {
      toast.error('Admin not added');
      console.log('Form validation failed');
    }
  };

  return (
    <StyledWrapper >
      <form className="form " onSubmit={handleSubmit}>
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

  @keyframes pulse {
    from {
      transform: scale(0.9);
      opacity: 1;
    }

    to {
      transform: scale(1.8);
      opacity: 0;
    }
  }
`;

export default Form;