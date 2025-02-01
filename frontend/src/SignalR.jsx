import { useEffect, useState } from "react";
import * as signalR from "@microsoft/signalr";

const useSignalR = (hubUrl, eventName, callback) => {
    const [connection, setConnection] = useState(null);

    useEffect(() => {
        const newConnection = new signalR.HubConnectionBuilder()
            .withUrl(hubUrl)
            .withAutomaticReconnect()
            .build();

        setConnection(newConnection);

        return () => {
            if (connection) {
                connection.stop();
            }
        };
    }, [hubUrl]);

    useEffect(() => {
        if (connection) {
            connection.start()
                .then(() => {
                    console.log("Connected to SignalR");

                    connection.on(eventName, callback);
                })
                .catch(err => console.error("Connection failed: ", err));
        }
    }, [connection, eventName, callback]);
};

export default useSignalR;
