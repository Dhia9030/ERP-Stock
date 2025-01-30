import {createContext , useContext , useState} from 'react';

const NotificationContext = createContext();


const  NotificationProvider = ({children}) => {
    const [notificationOn , setNotificationOn] = useState(true);

    const toggleNotification = ()=>{
        setNotificationOn(prev=>!prev);
        }

    return (
        <NotificationContext.Provider value={{notificationOn , toggleNotification}}>
            {children}
        </NotificationContext.Provider>
    )



}


const useNotification = ()=>{
    return useContext(NotificationContext);
}


export {NotificationProvider , useNotification};