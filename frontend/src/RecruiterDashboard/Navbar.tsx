import React from 'react';
import { Link } from 'react-router-dom';
import './NavBar.css';

const NavBar = () => {
    return (
        <nav className='navigimi'>
            <ul className='lista'>
                <li className='itemi'>
                    <Link className='link' to='/'>Home Page</Link>
                </li>
                <li className='itemi'>
                    <Link className='link' to='/jobDashboard'>Jobs</Link>
                </li>
                <li className='itemi'>
                    <Link className='link' to='/Companies'>Companies</Link>
                </li>
                <li className='itemi'>
                    <Link className='link' to='/jobApplications'>Applications</Link>
                </li>
            </ul>
        </nav>
    );
};

export default NavBar;

