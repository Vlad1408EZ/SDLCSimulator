import React from "react";
import clx from "classnames";
import cs from "../../../../scss/common.module.scss";

const capitalizeFirst = (str) =>
    `${str.slice(0, 1).toUpperCase()}${str.slice(1)}`;

const FlexBox = React.forwardRef((props, ref) => {
    const {
        children,
        clone,
        className: classNameProp,
        component: ComponentProp,

        alignItems,
        justifyContent,
        flexDirection,
        flexWrap,

        ...rest
    } = props;

    const className = clx(
        classNameProp,
        cs.flex,
        alignItems ? cs[`alignItems${capitalizeFirst(alignItems)}`] : null,
        justifyContent
            ? cs[`justifyContent${capitalizeFirst(justifyContent)}`]
            : null,
        flexDirection ? cs[`flexDirection${capitalizeFirst(flexDirection)}`] : null,
        flexWrap ? cs[`flexWrap${capitalizeFirst(flexWrap)}`] : null
    );

    if (clone) {
        const cloneProps = { className: clx(children.props.className, className) };
        return React.cloneElement(children, cloneProps);
    }

    const newProps = { ...rest, className };
    const FinalCompnent = ComponentProp || "div";
    return (
        <FinalCompnent ref={ref} {...newProps}>
            {children}
        </FinalCompnent>
    );
});

export default FlexBox;
