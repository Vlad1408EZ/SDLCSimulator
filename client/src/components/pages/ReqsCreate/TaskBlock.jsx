import { Draggable } from "react-beautiful-dnd";

import List from "@mui/material/List";
import ListItem from "@mui/material/ListItem";
import ListItemText from "@mui/material/ListItemText";

import IconButton from "@mui/material/IconButton";
import DeleteIcon from "@mui/icons-material/Delete";

const TaskBlock = ({
    block,
    blockIndex,
    onDelete,
    onItemDelete,
    dndPlaceholder,
}) => {
    return (
        <List
            sx={{ width: "100%", bgcolor: "background.paper" }}
            component="nav"
            aria-labelledby="nested-list-subheader"
            subheader={
                <ListItem
                    secondaryAction={
                        <IconButton aria-label="delete" edge="end" size="small" onClick={() => onDelete(blockIndex)}>
                            <DeleteIcon fontSize="inherit" />
                        </IconButton>
                    }
                >
                    {block?.name}
                </ListItem>
            }
        >
            {block.items && block.items.map((item, itemIndex) => (
                <Draggable index={itemIndex} draggableId={`${block.name}-${item.name}-${item.id}`}>
                    {(provided) => (
                        <div
                            {...provided.draggableProps}
                            {...provided.dragHandleProps}
                            ref={provided.innerRef}
                            style={provided.draggableProps.style}
                        >
                            <ListItem
                                secondaryAction={
                                    <IconButton aria-label="delete" edge="end" size="small" onClick={() => onItemDelete({ blockIndex, itemIndex })}>
                                        <DeleteIcon fontSize="inherit" />
                                    </IconButton>
                                }
                            >
                                <ListItemText primary={item.name} />
                            </ListItem>
                        </div>
                    )}
                </Draggable>
            ))}
            {dndPlaceholder}
        </List>
    )
};

export default TaskBlock;
