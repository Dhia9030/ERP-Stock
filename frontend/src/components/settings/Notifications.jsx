import { useState } from "react";
import SettingSection from "./SettingSection";
import { Bell } from "lucide-react";
import ToggleSwitch from "./ToggleSwitch";

import { useNotification } from "../../context/NotificationProvider";

const Notifications = () => {

	const{notificationOn , toggleNotification} = useNotification();

	return (
		<SettingSection icon={Bell} title={"Notifications"}>
			<ToggleSwitch
				label={"Side Notifications"}
				isOn={notificationOn}
				onToggle={() => toggleNotification()}
			/>
			
		</SettingSection>
	);
};
export default Notifications;
