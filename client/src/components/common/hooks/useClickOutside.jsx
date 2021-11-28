import { useEffect } from "react";

const useClickOutside = (handler, ...refs) => {
	useEffect(() => {
		const listener = (event) => {
			// Do nothing if clicking ref's element or descendent elements
			for (let i = 0; i < refs.length; i++) {
				if (!refs[i].current || refs[i].current.contains(event.target)) {
					return;
				}
			}
			handler(event);
		};

		document.addEventListener("mousedown", listener);
		document.addEventListener("touchstart", listener);

		return () => {
			document.removeEventListener("mousedown", listener);
			document.removeEventListener("touchstart", listener);
		};
	}, [refs, handler]);
};

export default useClickOutside;
