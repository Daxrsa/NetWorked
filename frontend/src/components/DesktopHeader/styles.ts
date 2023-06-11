import styled, { css } from 'styled-components';
import { GrLinkedin } from 'react-icons/gr';
import { FiUsers, FiMessageSquare } from 'react-icons/fi';
import { BsBriefcase } from 'react-icons/bs';
import { AiFillHome, AiOutlineBell, AiFillCaretDown } from 'react-icons/ai';
import { BiCart } from 'react-icons/bi';

export const Container = styled.div`
background: rgb(2,0,36);
background: radial-gradient(circle, rgba(2,0,36,1) 0%, rgba(7,46,146,1) 0%, rgba(9,9,121,1) 0%, rgba(12,80,180,1) 0%, rgba(50,145,244,1) 56%, rgba(0,212,255,1) 100%);
  padding: 0 30px;

  position: fixed;
  top: 0;
  left: 0;
  right: 0;
  z-index: 2;

  display: none;

  @media (min-width: 1180px) {
    display: block;
  }
`;

export const Wrapper = styled.div`
  display: flex;
  justify-content: space-between;
  flex: 1;

  max-width: 1128px;
  margin: 0 auto;
  height: 52px;

  .left,
  .right nav {
    display: flex;
    align-items: center;
  }

  .right nav {
    height: 100%;

    button {
      background: none;
      border: 0;
      outline: 0;

      display: flex;
      flex-direction: column;
      align-items: center;
      justify-content: center;
      min-width: 90px;
      min-height: 100%;

      color: var(--color-icons);
      cursor: pointer;

      &:hover {
        color: var(--color-white);
      }

      &.active {
        border-bottom: 2px solid lightblue;
      }

      &.notification {
        position: relative;

        &::after {
          width: 16px;
          height: 16px;
          position: absolute;
          left: 47px;
          top: 4px;
          background-color: red;
          border-radius: 13px;
          content: '1';
          color: #fff;
          border: 2px solid var(--color-header);
        }
      }
    }
  }
`;



const generalIconCSS = css`
  width: 20px;
  height: 20px;
`;

export const HomeIcon = styled(AiFillHome)`
  ${generalIconCSS}
`;

export const NetworkIcon = styled(FiUsers)`
  ${generalIconCSS}
`;

export const JobsIcon = styled(BsBriefcase)`
  ${generalIconCSS}
`;

export const MessagesIcon = styled(FiMessageSquare)`
  ${generalIconCSS}
`;

export const NotificationsIcon = styled(AiOutlineBell)`
  ${generalIconCSS}
`;
export const CartIcon = styled(BiCart)`
  ${generalIconCSS}
  
`;
export const ProfileCircle = styled.img`
  width: 24px;
  height: 24px;
  border-radius: 50%;
  border: 1px solid var(--color-icons);
`;

export const CaretDownIcon = styled(AiFillCaretDown)`
  width: 16px;
  height: 16px;
`;

export const DropdownMenu = styled.div`
  position: absolute;
  top: 99%;
  right:192px;
  display: flex;
  flex-direction: column;
  background-color: var(--color-header);
  border: 1px solid #cccccc;
  border-radius: 4px;
  padding: 8px;
  z-index: 999;
  width:100px;

  button {
    padding: 5px;
    border: none;
    background-color: black;
    cursor: pointer;

    &:hover {
      background-color: #f0f0f0;
    }
  }
`;
export const NotificationsDropdownMenu = styled.div`
  position: absolute;
  top: 99%;
  right:90px;
  display: flex;
  flex-direction: column;
  background-color: var(--color-header);
  border: 1px solid #cccccc;
  border-radius: 4px;
  padding: 8px;
  z-index: 999;
  width: 500px;

  button {
    padding: 5px;
    border: none;
    background-color: black;
    cursor: pointer;
   

    &:hover {
      background-color: #f0f0f0;
    }
  }
`;



