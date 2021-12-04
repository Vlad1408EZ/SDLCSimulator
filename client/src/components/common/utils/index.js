export const getUserRole = (role) => {
    if (role === "Student") return "Студент";
    if (role === "Teacher") return "Викладач";
    if (role === "Admin") return "Адмістратор";
};