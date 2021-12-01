import createNetwork from "./createNetwork";

export default createNetwork(process.env.REACT_APP_API_URI ? process.env.REACT_APP_API_URI : "https://localhost:44316/api");
